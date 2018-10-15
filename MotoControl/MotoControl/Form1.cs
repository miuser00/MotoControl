using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using UsbLibrary;
using System.IO.Ports;
using System.IO;

namespace MotorControl
{
    public enum CMD
    {
        Read,
        SMSpeed,
        SM,
        DCSpeed,
        StepSpeed,
        Step,
        Delay,
        Baund,
        ID,
        Action,
        Record,
        Stop,
        IICAddr,
        CycleCnt,
        ReadMem
    }
    public struct MSG
    {
        public CMD cmd;

        public byte ch;

        public int data;

        public MSG(CMD bytCmd, byte bytCh, int iData)
        {
            this.cmd = bytCmd;
            this.ch = bytCh;
            this.data = iData;
        }
    }


    public partial class Form1 : Form
    {

        public SteeringControl[] SteeringMotor = new SteeringControl[32];
        
        private MSG stcMSG;
        public byte byteID;
        public byte OnlineBoard;
        private Thread PortDetectThread;
        private FileWR file = new FileWR();
        private List<string> ButtonName = new List<string>();
        private List<string> DefaultPos = new List<string>();
        private List<string> FacePos = new List<string>();
        private bool PortStop = true;
        private string strCurrentFilePath;
        private int ActionIndex;
        private bool bListening;
        private bool bClosing;
        private byte[] RxBuffer = new byte[1000];
        private delegate void DecodeDataHandler(byte[] byteTemp, ushort usLength);
        private ushort usRxCnt;
        private ushort usTotalLength = 5;
        private Config formConfig;
        public ushort usOnboardID = 65535;
        public bool b_AppClosing=false;
        private bool bCheckID = true;
        private byte OfflineCnt;



        int L, H, Distance; //人脸的位置
        Posture lastPos = new Posture();  //上一次移动后的手臂姿态
        Posture limitPos_Low = new Posture();
        Posture limitPos_High = new Posture();

        long l_LastFaceDetection = 0;
        bool b_IsFaceLocked = false;
        bool b_IsFaceCentral = false;



        //控制部分
        ARM6 arm;
        bool b_MouseRelease = true;



        public class Posture
        {
            public int HLR = 0;     //头左右
            public int HUD = 0; //头上下
            public int HTLR = 0; //头旋转

            public int BLR = 0;  //身体左右
            public int BUD = 0;  //身体上下
            public int BFN = 0; //身体远近

        }


        public Form1()
        {

            InitializeComponent();
            arm = new ARM6("arm00",SendMessage);
            try
            {
                List<string> cmd = new List<string>();
                cmd = file.ReadToList("MotoCmd.ini");
                l_lastCmdSEQ = long.Parse(cmd[4].ToString());
            }
            catch
            { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SteeringMotor[0] = new SteeringControl(4);
            this.SteeringMotor[1] = new SteeringControl(3);
            this.SteeringMotor[2] = new SteeringControl(5);
            this.SteeringMotor[3] = new SteeringControl(0);
            this.SteeringMotor[4] = new SteeringControl(1);
            this.SteeringMotor[5] = new SteeringControl(2);

            this.SteeringMotor[14] = new SteeringControl(14);

            for (byte b = 0; b < 6; b += 1)
            {


                this.SteeringMotor[(int)b].SendMessage = new SteeringControl.SendMSG(this.SendMessage);
                this.SteeringMotor[(int)b].Show();
                this.pan_Control.Controls.Add(this.SteeringMotor[(int)b]);
                this.SteeringMotor[(int)b].Location = new Point((int)(b % 3) * this.pan_Control.Size.Width / 3+20, (int)(b / 3) * this.pan_Control.Size.Height / 2+30);
                this.PortDetectThread = new Thread(new ThreadStart(this.SetPort));
                this.PortDetectThread.Start();

                List<string> list = this.file.ReadToList("Config.ini");
                ButtonName = this.file.ReadToList("ButtonName.txt");
                DefaultPos= this.file.ReadToList("Default.ini");

                

                if (list == null)
                {
                    return;
                }
                this.strCurrentFilePath = list[0];

                this.spSerialPort.PortName = list[1];
                this.spSerialPort.BaudRate = int.Parse(list[2]);
                this.byteID = byte.Parse(list[3]);
                this.ActionIndex = int.Parse(list[4]);
                this.PortStop = false;
            }

            this.SteeringMotor[14].SendMessage = new SteeringControl.SendMSG(this.SendMessage);
            //头部极限位置
            limitPos_Low.HLR = 1000;
            limitPos_High.HLR = 2000;
            //头部上下极限位置
            limitPos_Low.HUD = 1000;
            limitPos_High.HUD = 2000;
            //头部旋转极限位置
            limitPos_Low.HTLR = 1000;
            limitPos_High.HTLR = 2000;

            //身体左右极限位置
            limitPos_Low.BLR = 1000;
            limitPos_High.BLR = 2000;
            //身体上下极限位置
            limitPos_Low.BUD = 1300;
            limitPos_High.BUD = 1700;
            //身体远近极限位置
            limitPos_Low.BFN = 1300;
            limitPos_High.BFN = 1700;

            tmr_RefreshARMStatus.Enabled = true;
        }

        public void SendMessage(byte cmd, byte ch, int data)
        {
            this.stcMSG.cmd = (CMD)((int)cmd | (int)this.byteID << 4);
            this.stcMSG.ch = ch;
            this.stcMSG.data = data;
            this.Send((byte)this.stcMSG.cmd, this.stcMSG.ch, this.stcMSG.data);
        }
        public void Send(byte cmd, byte ch, int data)
        {
            byte[] array = new byte[5];
            short num = (short)data;
            array[0] = 255;
            array[1] = cmd;
            array[2] = ch;
            array[3] = (byte)(num & 255);
            array[4] = (byte)(num >> 8);
            try
            {
                this.SendUSBMsg(3, array, (byte)array.Length);
                if (this.spSerialPort.IsOpen)
                {
                    this.spSerialPort.Write(array, 0, 5);
                }
                if (cmd != 0)
                {
                    if (this.OnlineBoard == 0)
                    {
                        this.Status1.Text = string.Concat(new string[]
                        {
                            this.GetLocalText("端口未打开！待发送数据：", "Port not open, datas to send are:"),
                            array[0].ToString("x"),
                            " ",
                            array[1].ToString("x"),
                            " ",
                            array[2].ToString("x"),
                            " ",
                            array[3].ToString("x"),
                            " ",
                            array[4].ToString("x"),
                            " "
                        });
                    }
                    else
                    {
                        this.Status1.Text = string.Concat(new string[]
                        {
                            this.GetLocalText("已发送：", "Send:"),
                            array[0].ToString("x"),
                            " ",
                            array[1].ToString("x"),
                            " ",
                            array[2].ToString("x"),
                            " ",
                            array[3].ToString("x"),
                            " ",
                            array[4].ToString("x"),
                            " "
                        });
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            try
            {
                base.OnHandleCreated(e);
                this.usb.RegisterHandle(base.Handle);
            }
            catch (Exception)
            {
            }
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                this.usb.ParseMessages(ref m);
                base.WndProc(ref m);
            }
            catch (Exception)
            {
            }
        }
        private sbyte SendUSBMsg(byte ucType, byte[] byteSend, byte ucLength)
        {
            try
            {
                if (this.usb.SpecifiedDevice != null)
                {
                    byte[] array = new byte[67];
                    array[1] = ucLength;
                    array[2] = ucType;
                    byteSend.CopyTo(array, 3);
                    this.usb.SpecifiedDevice.SendData(array);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return 0;
        }
        		private string GetLocalText(string ChString, string EnString)
		{
			if (Thread.CurrentThread.CurrentUICulture.Name == "zh-CN")
			{
				return ChString;
			}
			return EnString;
		}
        private void SetPort()
        {
            while (b_AppClosing)
            {
                Thread.Sleep(500);
                if (!this.PortStop)
                {
                    if (!this.spSerialPort.IsOpen)
                    {
                        this.OnlineBoard &= 254;
                        try
                        {
                            DateTime arg_37_0 = DateTime.Now;
                            this.spSerialPort.Open();
                            this.OnlineBoard |= 1;
                            DateTime arg_57_0 = DateTime.Now;
                            continue;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    this.OnlineBoard |= 1;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            List<string> list = new List<string>();
            list.Add(this.strCurrentFilePath);
            list.Add(this.spSerialPort.PortName);
            list.Add(this.spSerialPort.BaudRate.ToString());
            list.Add(this.ActionIndex.ToString());
            list.Add(0.ToString());
            this.file.WriteToFile(list, "Config.ini");
            this.tmr_USBConn.Stop();
            b_AppClosing = true;
            this.PortClose(null, null);
            this.PortDetectThread.Abort();


        }
        private void PortClose(object sender, EventArgs e)
        {
            this.PortStop = true;
            if (this.spSerialPort.IsOpen)
            {
                this.bClosing = true;
                while (this.bListening)
                {
                    Application.DoEvents();
                }
                this.spSerialPort.Dispose();
                this.spSerialPort.Close();
            }
        }


        private void usb_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            if (base.InvokeRequired)
            {
                try
                {
                    base.Invoke(new DataRecievedEventHandler(this.usb_OnDataRecieved), new object[]
                    {
                        sender,
                        args
                    });
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            byte b = args.data[1];
            switch (args.data[2])
            {
                case 0:
                    break;
                case 1:
                    for (int i = 0; i < (int)b; i++)
                    {
                        args.data[i] = args.data[i + 3];
                    }
                    this.DecodeData(args.data, (ushort)b);
                    break;
                default:
                    return;
            }
        }


        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            this.Status1.Text = "My Device Connected!";
            this.OnlineBoard |= 2;
            this.SetBaudrate(this.spSerialPort.BaudRate);
        }

        private void usb_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            this.Status1.Text = "My Device DisConnected!";
            this.OnlineBoard &= 253;
        }

        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            this.Status1.Text = "Find USB Device!";
        }

        private void usb_OnDeviceRemoved(object sender, EventArgs e)
        {
            this.Status1.Text = "USB Device Removed!";
        }
        private void ByteCopy(byte[] byteFrom, byte[] byteTo, ushort usFromIndex, ushort usToIndex, ushort usLength)
        {
            for (int i = 0; i < (int)usLength; i++)
            {
                byteTo[(int)usToIndex + i] = byteFrom[(int)usFromIndex + i];
            }
        }

        public void DecodeData(byte[] byteTemp, ushort usLength)
        {
            if (base.InvokeRequired)
            {
                try
                {
                    base.Invoke(new Form1.DecodeDataHandler(this.DecodeData), new object[]
                    {
                        byteTemp,
                        usLength
                    });
                }
                catch (Exception)
                {
                }
                return;
            }
            this.ByteCopy(byteTemp, this.RxBuffer, 0, this.usRxCnt, usLength);
            this.usRxCnt += usLength;
            while (this.usRxCnt >= this.usTotalLength)
            {
                if (!this.CheckHead(this.RxBuffer, new byte[]
                {
                    255,
                    240
                }, 2))
                {
                    this.ByteCopy(this.RxBuffer, this.RxBuffer, 1, 0, this.usRxCnt);
                    this.usRxCnt -= 1;
                }
                else
                {
                    if (this.formConfig != null)
                    {
                        this.formConfig.DecodeData(this.RxBuffer);
                    }
                    if (this.RxBuffer[1] == 240)
                    {
                        short num = BitConverter.ToInt16(this.RxBuffer, 3);
                        if (num == 0 | (num & 255) == 255)
                        {
                            this.usOnboardID = 0;
                        }
                    }
                    this.ByteCopy(this.RxBuffer, this.RxBuffer, this.usTotalLength, 0, (ushort)(this.usRxCnt - this.usTotalLength));
                    this.usRxCnt -= this.usTotalLength;
                }
            }
        }

        private void SetBaudrate(int iBaund)
        {
            try
            {
                this.spSerialPort.BaudRate = iBaund;
                byte[] array = new byte[4];
                if (iBaund <= 38400)
                {
                    if (iBaund <= 4800)
                    {
                        if (iBaund == 2400)
                        {
                            array[1] = 1;
                            goto IL_DC;
                        }
                        if (iBaund == 4800)
                        {
                            array[1] = 2;
                            goto IL_DC;
                        }
                    }
                    else
                    {
                        if (iBaund == 9600)
                        {
                            array[1] = 3;
                            goto IL_DC;
                        }
                        if (iBaund == 19200)
                        {
                            array[1] = 4;
                            goto IL_DC;
                        }
                        if (iBaund == 38400)
                        {
                            array[1] = 5;
                            goto IL_DC;
                        }
                    }
                }
                else if (iBaund <= 230400)
                {
                    if (iBaund == 57600)
                    {
                        array[1] = 6;
                        goto IL_DC;
                    }
                    if (iBaund == 115200)
                    {
                        array[1] = 7;
                        goto IL_DC;
                    }
                    if (iBaund == 230400)
                    {
                        array[1] = 8;
                        goto IL_DC;
                    }
                }
                else
                {
                    if (iBaund == 460800)
                    {
                        array[1] = 9;
                        goto IL_DC;
                    }
                    if (iBaund == 921600)
                    {
                        array[1] = 10;
                        goto IL_DC;
                    }
                    if (iBaund == 1382400)
                    {
                        array[1] = 11;
                        goto IL_DC;
                    }
                }
                array[1] = 0;
            IL_DC:
                array[2] = 0;
                array[0] = 2;
                this.SendUSBMsg(2, array, 3);
                Thread.Sleep(100);
                array[0] = 3;
                this.SendUSBMsg(2, array, 3);
                Thread.Sleep(100);
                array[0] = 4;
                this.SendUSBMsg(2, array, 3);
            }
            catch (Exception)
            {
            }
        }

        private bool CheckHead(byte[] byteData, byte[] byteHeadTemp, int byteHeadLength)
        {
            byte b = 0;
            while ((int)b < byteHeadLength)
            {
                if (byteData[(int)b] != byteHeadTemp[(int)b])
                {
                    return false;
                }
                b += 1;
            }
            return true;
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] array = new byte[1000];
            try
            {
                ushort usLength = (ushort)this.spSerialPort.Read(array, 0, 500);
                this.DecodeData(array, usLength);
            }
            catch (Exception)
            {
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.OnlineBoard == 0)
            {
                this.textBoxBoard.Text = this.GetLocalText("花之眼离线", "Offline");
                this.textBoxBoard.BackColor = Color.Yellow;
                return;
            }
            if (this.bCheckID)
            {
                this.bCheckID = false;
                this.usOnboardID = 65535;
                this.Send(0, 18, 0);
                return;
            }
            this.bCheckID = true;
            if (this.usOnboardID == 65535)
            {
                this.OfflineCnt += 1;
            }
            else
            {
                this.OfflineCnt = 0;
            }
            if (this.usOnboardID == 65535)
            {
                this.textBoxBoard.Text = this.GetLocalText("花之眼离线", "Offline");
            }
            else
            {
                this.textBoxBoard.Text = this.GetLocalText("花之眼在线", "Online");
            }
            if (this.textBoxBoard.Text == this.GetLocalText("花之眼离线", "Offline"))
            {
                this.textBoxBoard.BackColor = Color.Yellow;
                return;
            }
            if (((int)this.usOnboardID & 1 << (int)this.byteID) == 0)
            {
                this.textBoxBoard.BackColor = Color.Black;
            }
        }


        void SetHLR(int pos,int speed)  //// pos -1000 - +1000  对应500-2500的最大范围
        {
            pos = pos + lastPos.HLR;
            //通道4 头部左右
            SendMessage(1, 4, (int)(speed));

            int i_newPos = (int)(pos + 1500);
            if ((i_newPos < limitPos_High.HLR) && (i_newPos > limitPos_Low.HLR))
            {
                SendMessage(2, 4, i_newPos);
                lastPos.HLR = pos;
            }
        }

        void SetHUD(int pos, int speed) //// pos -1000 - +1000  对应500-2500的最大范围
        {
            pos = pos + lastPos.HUD;
            //通道3 头部高低
            SendMessage(1, 3, (int)(speed ));

            int i_newPos = (int)(pos+ 1500);
            if ((i_newPos < limitPos_High.HUD) && (i_newPos > limitPos_Low.HUD))
            {
                SendMessage(2, 3, i_newPos);
                lastPos.HUD = pos;
            }
        }
        void SetTLR(int pos, int speed) //// pos -1000 - +1000  对应500-2500的最大范围
        {
            pos = pos + lastPos.HTLR;
            //通道5 头部左右旋转
            SendMessage(1, 5, (int)(speed ));

            int i_newPos = (int)(pos + 1500);
            if ((i_newPos < limitPos_High.HTLR) && (i_newPos > limitPos_Low.HTLR))
            {
                SendMessage(2, 5, i_newPos);
                //lastPos.HTLR = pos;
            }

        }
        void SetBLR(int pos, int speed) //// pos -1000 - +1000  对应500-2500的最大范围
        {
            pos = pos + lastPos.BLR;
            //通道0 身体左右
            SendMessage(1, 0, (int)(speed ));

            int i_newPos = (int)(pos + 1500);
            if ((i_newPos < limitPos_High.BLR) && (i_newPos > limitPos_Low.BLR))
            {
                SendMessage(2, 0, i_newPos);
                lastPos.BLR = pos;
            }
        }
        void SetBUD(int pos, int speed) //// pos -100 - +100 ,d_rate (0.1-2) 对应500-2500的最大范围
        {
            //pos = pos * (-1); //头高低是反向的
            pos = pos + lastPos.BUD;
            //通道1 身体上下
            SendMessage(1, 1, (int)(speed ));

            int i_newPos = (int)(pos + 1500);
            if ((i_newPos < limitPos_High.BUD) && (i_newPos > limitPos_Low.BUD))
            {
                SendMessage(2, 1, i_newPos);
                lastPos.BUD = pos;
            }
        }
        void SetBFN(int pos, int speed) //// pos -100 - +100 ,d_rate (0.1-2) 对应500-2500的最大范围
        {
            pos = pos + lastPos.BFN;
            //通道2 身体前后

            SendMessage(1, 2, (int)(speed ));
            int i_newPos = (int)(pos  + 1500);
            if ((i_newPos < limitPos_High.BFN) && (i_newPos > limitPos_Low.BFN))
            {
                SendMessage(2, 2, i_newPos);
                lastPos.BFN = pos;
            }
        }






        private void btn_SaveZero_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认是否要把当前位置设置为0位", "请确认您的操作", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                for (int i = 0; i < 6; i++)
                {
                    DefaultPos[i] = SteeringMotor[i].GetAngle().ToString();
                    
                }
                file.WriteToFile(DefaultPos, "default.ini");
            }
        }



        private void btn_BodyMove_Click(object sender, EventArgs e)
        {
            if ((Math.Abs(lastPos.HLR) > 20) || (Math.Abs(lastPos.HUD) > 20))
            {

                //通过移动身体补偿图像
                SetBLR((int)(lastPos.HLR), 2);
                SetBUD((int)(lastPos.HUD*4), 2);

                //头部恢复原位
                lastPos.HLR = 0;
                lastPos.HUD = 0;
                SetHLR(0, 5);
                SetHUD(0, 5);


            }
        
            if (Math.Abs(Distance)>100 )SetBFN((int)(-Distance*0.5), 5);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void tmr_GetHead_Tick(object sender, EventArgs e)
        {

        }

        private void tmr_Body_Tick(object sender, EventArgs e)
        {
            btn_BodyMove_Click(sender, e);
        }

        private void btn_Watch_Click(object sender, EventArgs e)
        {
            if (tmr_Watch.Enabled == true)
            {
                btn_Watch.Text = "观察";
                tmr_Watch.Enabled = false;
            }
            else
            {
                btn_Watch.Text = "停止观察";
                tmr_Watch.Enabled = true;
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }
        double d_Head_LR_last = 0;
        double d_Head_UD_last = 0;
        double d_Head_SLR_last = 0;
        double d_Body_LR_last = 0;
        double d_Body_UD_last = 0;
        double d_Body_FN_last = 0;

        private void tmr_RefreshARMStatus_Tick(object sender, EventArgs e)
        {
            //刷新手臂位置

            
            txt_Head_LR.Text = arm.Head_LR.pos.ToString("F1");
            txt_Head_LR_Speed.Text = arm.Head_LR.speed.ToString("F1");

            txt_Head_UD.Text = arm.Head_UD.pos.ToString("F1");
            txt_Head_UD_Speed.Text = arm.Head_UD.speed.ToString("F1");

            txt_Head_SLR.Text = arm.Head_SLR.pos.ToString("F1");
            txt_Head_SLR_Speed.Text = arm.Head_SLR.speed.ToString("F1");

            txt_Neck_LR.Text = arm.Body_LR.pos.ToString("F1");
            txt_Neck_LR_Speed.Text = arm.Body_LR.speed.ToString("F1");

            txt_Neck_UD.Text = arm.Body_UD.pos.ToString("F1");
            txt_Neck_UD_Speed.Text = arm.Body_UD.speed.ToString("F1");

            txt_Neck_FN.Text = arm.Body_FN.pos.ToString("F1");
            txt_Neck_FN_Speed.Text = arm.Body_FN.speed.ToString("F1");

            if ((d_Head_LR_last == arm.Head_LR.pos) && (d_Head_UD_last == arm.Head_UD.pos) && (d_Head_SLR_last == arm.Head_SLR.pos) && (d_Body_LR_last == arm.Body_LR.pos) && (d_Body_UD_last == arm.Body_UD.pos) && (d_Body_FN_last == arm.Body_FN.pos))
            {
                lab_movement.Text = "静止";
            }
            else
            {
                d_Head_LR_last = arm.Head_LR.pos;
                d_Head_UD_last = arm.Head_UD.pos;
                d_Head_SLR_last = arm.Head_SLR.pos;

                d_Body_LR_last = arm.Body_LR.pos;
                d_Body_UD_last = arm.Body_UD.pos;
                d_Body_FN_last = arm.Body_FN.pos;
                lab_movement.Text = "移动中";
            }

        }

        private void btn_Head_UP_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_UD(tbHeadSpeed.Value, 10);
            Log("头部上移动" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部上移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_UP_MouseDown(sender, e);
        }

        private void btn_Head_Down_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_UD(tbHeadSpeed.Value*(-1), 10);
            Log("头部下移动" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部下移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_Down_MouseDown(sender, e);
        }

        private void btn_Head_LT_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_LR(tbHeadSpeed.Value *(- 1), 10);
            Log("头部左转" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部左转";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_LT_MouseDown(sender, e);
        }

        private void btn_Head_RT_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_LR(tbHeadSpeed.Value, 10);
            Log("头部右转" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部右转";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_RT_MouseDown(sender, e);
        }

        private void btn_Head_LS_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_SLR(tbHeadSpeed.Value , 10);
            Log("头部左歪" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部左歪";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_LS_MouseDown(sender, e);
        }

        private void btn_Head_RS_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Head_SLR(tbHeadSpeed.Value*(-1), 10);
            Log("头部右歪" + "步长" + tbHeadSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "头部右歪";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Head_RS_MouseDown(sender, e);
        }

        private void btn_Neck_UP_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_UD(tb_NeckSpeed.Value, 10);
            Log("颈部上移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部上移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_UP_MouseDown(sender, e);
        }

        private void btn_Neck_Down_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_UD(tb_NeckSpeed.Value *(- 1), 10);
            Log("颈部下移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部下移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_Down_MouseDown(sender, e);
        }

        private void btn_Neck_LT_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_LR(tb_NeckSpeed.Value *(- 1), 10);
            Log("颈部左移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部左移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_LT_MouseDown(sender, e);
        }

        private void btn_Neck_RT_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_LR(tb_NeckSpeed.Value, 10);
            Log("颈部右移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部右移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_RT_MouseDown(sender, e);
        }

        private void btn_Neck_FT_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_FN(tb_NeckSpeed.Value, 10);
            Log("颈部前移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部前移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_FT_MouseDown(sender, e);
        }

        private void btn_Neck_BH_MouseDown(object sender, MouseEventArgs e)
        {
            //按键按下
            b_MouseRelease = false;
            arm.Move_Body_FN(tb_NeckSpeed.Value *(- 1), 10);
            Log("颈部后移动" + "步长" + tb_NeckSpeed.Value.ToString() + " 速度" + 10.ToString());
            lab_status.Text = "颈部后移动";
            Thread.Sleep(10);
            Application.DoEvents();
            if (!b_MouseRelease) btn_Neck_BH_MouseDown(sender, e);
        }

        private void btn_ResetARM_Click(object sender, EventArgs e)
        {
            Log("机械臂恢复初始位置");
            lab_status.Text = "机械臂复位";
            Application.DoEvents();
            arm.Set_Head_LR(arm.s_Head_LR.DefaultPos, 10);
            arm.Set_Head_UD(arm.s_Head_UD.DefaultPos, 10);
            arm.Set_Head_SLR(arm.s_Head_SLR.DefaultPos, 10);

            arm.Set_Body_LR(arm.s_Body_LR.DefaultPos, 10);
            arm.Set_Body_UD(arm.s_Body_UD.DefaultPos, 10);
            arm.Set_Body_FN(arm.s_Body_FN.DefaultPos, 10);
            
            lab_status.Text = "指令结束";

        }

        private void rad_Head_UD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_Head_LR_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void btn_SetLowLimit_Click(object sender, EventArgs e)
        {
            if (rad_HLR.Checked)
            {
                arm.SetPosAsJointLowLimit(0);
            }
            if (rad_HUD.Checked)
            {
                arm.SetPosAsJointLowLimit(1);
            }
            if (rad_HTLR.Checked)
            {
                arm.SetPosAsJointLowLimit(2);
            }

            if (rad_NLR.Checked)
            {
                arm.SetPosAsJointLowLimit(3);
            }
            if (rad_NUD.Checked)
            {
                arm.SetPosAsJointLowLimit(4);
            }
            if (rad_NFN.Checked)
            {
                arm.SetPosAsJointLowLimit(5);
            }
        }

        private void btn_SetHighLimit_Click(object sender, EventArgs e)
        {
            if (rad_HLR.Checked)
            {
                arm.SetPosAsJointHighLimit(0);
            }
            if (rad_HUD.Checked)
            {
                arm.SetPosAsJointHighLimit(1);
            }
            if (rad_HTLR.Checked)
            {
                arm.SetPosAsJointHighLimit(2);
            }

            if (rad_NLR.Checked)
            {
                arm.SetPosAsJointHighLimit(3);
            }
            if (rad_NUD.Checked)
            {
                arm.SetPosAsJointHighLimit(4);
            }
            if (rad_NFN.Checked)
            {
                arm.SetPosAsJointHighLimit(5);
            }
        }

        private void btn_SetDefaultPos_Click(object sender, EventArgs e)
        {
            arm.SetPosAsDefault();
        }

        private void btn_Head_UP_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Head_Down_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Head_LT_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Head_RT_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Head_LS_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Head_RS_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_UP_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_Down_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_LT_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_RT_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_FT_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void btn_Neck_BH_MouseUp(object sender, MouseEventArgs e)
        {
            b_MouseRelease = true;
            lab_status.Text = "指令结束";
        }

        private void trb_Calc_Scroll(object sender, EventArgs e)
        {
            if (rad_HLR.Checked)
            {
                arm.Set_Joint_Raw(0, trb_Calc.Value, 10);
            }
            if (rad_HUD.Checked)
            {
                arm.Set_Joint_Raw(1, trb_Calc.Value, 10);
            }
            if (rad_HTLR.Checked)
            {
                arm.Set_Joint_Raw(2, trb_Calc.Value, 10);
            }

            if (rad_NLR.Checked)
            {
                arm.Set_Joint_Raw(3, trb_Calc.Value, 10);
            }
            if (rad_NUD.Checked)
            {
                arm.Set_Joint_Raw(4, trb_Calc.Value, 10);
            }
            if (rad_NFN.Checked)
            {
                arm.Set_Joint_Raw(5, trb_Calc.Value, 10);
            }
        }

        private void btn_SaveChannel_Click(object sender, EventArgs e)
        {
            if (rad_HLR.Checked)
            {
                arm.Save(0);
            }
            if (rad_HUD.Checked)
            {
                arm.Save(1);
            }
            if (rad_HTLR.Checked)
            {
                arm.Save(2);
            }

            if (rad_NLR.Checked)
            {
                arm.Save(3);
            }
            if (rad_NUD.Checked)
            {
                arm.Save(4);
            }
            if (rad_NFN.Checked)
            {
                arm.Save(5);
            }
        }

        private void btn_SaveAllChannel_Click(object sender, EventArgs e)
        {
            arm.Save(-1);
        }

        private void btn_SaveDefaultPos_Click(object sender, EventArgs e)
        {
            arm.Save(-2);
        }

        private void btn_Neck_UP_Click(object sender, EventArgs e)
        {

        }

        private void btn_Neck_LT_Click(object sender, EventArgs e)
        {

        }

        private void btn_Head_UP_Click(object sender, EventArgs e)
        {

        }

        private void tmr_RefreshFaceStatus_Tick(object sender, EventArgs e)
        {
            //从脸部识别获取头像偏移量
            btn_GetBias_Click(sender, e);
            if ((L == 999) && (H == 999) && (Distance == 999))
            {
                b_IsFaceLocked = false;
                txt_FaceLocked.BackColor = Color.Red;
                //pan_FaceSearch.BackColor = Color.Gray;
            }
            else
            {
                b_IsFaceLocked = true;
                txt_FaceLocked.BackColor = Color.Green;
                //pan_FaceSearch.BackColor = Color.YellowGreen;
            }

            if ((Math.Abs(L)<20)&&(Math.Abs(H)<20)&&b_IsFaceLocked)
            {
                b_IsFaceCentral = true;
                txt_FaceCentral.BackColor = Color.Green;
                //pan_FaceSearch.BackColor = Color.DarkGreen;
            }
            else
            {
                b_IsFaceCentral = false;
                txt_FaceCentral.BackColor = Color.Red;
            }

        }

        private void btn_NearPilot_Click(object sender, EventArgs e)
        {            
            if (tmr_NearPilot.Enabled ==true)
            {
                btn_NearPilot.Text = "近程巡视";
                tmr_NearPilot.Enabled = false;
            }else
            {
                btn_NearPilot.Text = "停止巡视";
                tmr_NearPilot.Enabled = true;
            }
        }

        private int farPilotdirection=1, nearPilotdirection=1;

        private void btn_FarPilot_Click(object sender, EventArgs e)
        {

            if (tmr_FarPilot.Enabled == true)
            {
                btn_FarPilot.Text = "远程巡视";
                tmr_FarPilot.Enabled = false;
            }
            else
            {
                btn_FarPilot.Text = "停止巡视";
                tmr_FarPilot.Enabled = true;
            }
        }
        int i_waveCount = 100;
        private void tmr_FaceFollow_Tick(object sender, EventArgs e)
        {
            if (b_IsFaceLocked == true)
            {
                //tmr_RefreshFaceStatus.Enabled = false;
                //先移动头部
                if (Math.Abs(L) > 10)
                {
                    arm.Move_Head_LR(-L / 10, 5);
                }
                if (Math.Abs(H) > 10)
                {
                    arm.Move_Head_UD(H / 10, 5);
                }
                //如果头部偏移过多，用躯干补偿
                if (Math.Abs(arm.Head_LR.pos) > 30)
                {
                    arm.Move_Body_LR(arm.Head_LR.pos, 1);
                    arm.Move_Head_LR(arm.Head_LR.pos * (-1), 1);
                }
                
            }

        }
        private double WatchDirection = 1;
        private void tmr_Watch_Tick(object sender, EventArgs e)
        {
            //如果走到位置 +50 or -50 就反向
            if ((Math.Abs(arm.Head_SLR.pos))>=30)
            {
                if (arm.Head_SLR.pos > 0) WatchDirection = -1; else WatchDirection = 1;
            }
            arm.Move_Head_SLR(WatchDirection, 2);
            i_waveCount++;

        }

        private void btn_FaceFollow_Click(object sender, EventArgs e)
        {
            if (tmr_FaceFollow.Enabled == true)
            {
                btn_FaceFollow.Text = "开始寻脸";
                tmr_FaceFollow.Enabled = false;
                tmr_Watch.Enabled = false;
            }
            else
            {
                btn_FaceFollow.Text = "停止寻脸";
                tmr_FaceFollow.Enabled = true;
            }
        }

        private void tmr_FarPilot_Tick(object sender, EventArgs e)
        {
            //头部不在正中，且身体也不在极限位置
            if ((Math.Abs((Math.Abs(arm.Body_LR.pos) - 100)) > 0.01) && ((Math.Abs(arm.Head_LR.pos)) > 0.01))
            {
                //把头放正
                arm.Set_Head_LR(0, 20);
            }


            //仅当头部放正再移动身体
            if (Math.Abs((Math.Abs(arm.Head_LR.pos))) < 0.01)
            {
                arm.Move_Body_LR(farPilotdirection, 2);
            }

            //如果躯干到达极限位置，头部继续偏移
            if (Math.Abs((Math.Abs(arm.Body_LR.pos) - 100)) < 0.01)
            {
                arm.Move_Head_LR(farPilotdirection, 2);
                if (Math.Abs((Math.Abs(arm.Head_LR.pos) - 100)) < 0.01)
                {
                    farPilotdirection = farPilotdirection * (-1);
                }
            }
        }

        int i_LockCount = 0; //锁定时间
        int i_UnlockCount = 0;
        int i_CentralCount = 0; //居中判断
        int i_UnCentralCount = 0;
        private void tmr_Run_Tick(object sender, EventArgs e)
        {
            lab_status.Text = "自动运行中";
            //判断 状态
            if (b_IsFaceLocked == true)
            {
                i_UnlockCount = -1;
                i_LockCount++;
            }else
            {
                i_LockCount = -1;
                i_UnlockCount++;
            }
            if (b_IsFaceCentral ==true)
            {
                i_UnCentralCount = -1;
                i_CentralCount++;
            }
            else
            {
                i_CentralCount = -1;
                i_UnCentralCount++;
            }

            if (i_UnlockCount>95&&i_UnlockCount<100)
            {
                btn_ResetARM_Click(sender, e);
            }

            //很长时间没有锁定目标则开始巡逻
            if (i_UnlockCount>100)
            {
                if (i_UnlockCount/10 % 10 <5)
                {
                    tmr_FarPilot.Enabled = true;
                    tmr_NearPilot.Enabled = false;
                }
                else
                {
                    tmr_FarPilot.Enabled = false;
                    tmr_NearPilot.Enabled = true;
                }
            }

            if (b_IsFaceLocked==true)
            {
                tmr_FarPilot.Enabled = false;
                tmr_NearPilot.Enabled = false;
                tmr_FaceFollow.Enabled = true;
                
            }else
            {
                tmr_FaceFollow.Enabled = false;
            }




            if (i_CentralCount > 10 && i_CentralCount < 30)
            {

                //如果居中，则摇头一会

                //摇晃一下脑袋
                if (b_IsFaceCentral)
                {
                    tmr_Watch.Enabled = true;
                    i_waveCount = 0;

                }
            }
            else
            {
                tmr_Watch.Enabled = false;
            }
            //图像调整后动一下脖子
            if (i_CentralCount == 30)
            {
                //arm.Move_Body_FN(-25, 5);

            }
            if (i_CentralCount == 35)
            {
                //arm.Move_Body_FN(25, 5);
            }
            if (i_CentralCount>35)
            {
                arm.Set_Head_SLR(0, 5);
            }
        }

        private void btn_AUTO_ARM_Click(object sender, EventArgs e)
        {
            tmr_Run.Enabled = true;
            lab_status.Text = "自动运行中";
        }

        private void btn_MANUAL_ARM_Click(object sender, EventArgs e)
        {
            tmr_Run.Enabled = false;
            tmr_FarPilot.Enabled = false;
            tmr_NearPilot.Enabled = false;
            tmr_FaceFollow.Enabled = false;
            lab_status.Text = "手动运行中";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void tmr_NearPilot_Tick(object sender, EventArgs e)
        {
            //如果走到极限位置 +100 or -100 就反向
            if (Math.Abs((Math.Abs(arm.Head_LR.pos) - 100))<0.01)
            {
                nearPilotdirection = nearPilotdirection * (-1);
            }
            arm.Move_Head_LR(nearPilotdirection,2);
        }
        int i_missFaceDataCount = 0; //没有收到面部数据的次数

        long l_lastCmdSEQ = 0;
        long reportno = 0;
        private void btn_GetCmd_Click(object sender, EventArgs e)
        {
            //////////////数据格式
            // 指令 0  AUTO MANUAL RESET MOVE SET
            // 参数 1  UD LR FN HUD HLR HSLR
            // 参数 2  STEP or POS
            // 参数3   SPEED  
            // 序列号 4

            try
            {

                List<string> cmd = new List<string>();
                cmd = file.ReadToList("MotoCmd.ini");
                long l_currCmdSEQ = long.Parse(cmd[4].ToString());

                //接收到新指令
                if (l_lastCmdSEQ != l_currCmdSEQ)
                {
                    l_lastCmdSEQ = l_currCmdSEQ;
                    Log("发现新指令");

                    if (cmd[0] == "AUTO")
                    {
                        //停止自动化
                        btn_AUTO_ARM_Click(sender, e);
                    }
                    if (cmd[0] == "MANUAL")
                    {
                        btn_MANUAL_ARM_Click(sender, e);
                    }
                    if (cmd[0] == "RESET")
                    {
                        btn_ResetARM_Click(sender, e);
                        Log("机械臂恢复初始位置");
                        Application.DoEvents();
                    }
                    if (cmd[0]=="MOVE")
                    {
                        double step = Convert.ToDouble(cmd[2]);
                        double speed=Convert.ToDouble(cmd[3]);
                        if (cmd[1]=="UD")
                        {
                            arm.Move_Body_UD(step,speed);
                            Log("颈部上下移动" + "步长" + step.ToString()+" 速度"+ speed.ToString());
                            lab_status.Text = "遥控颈部上下移动";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "LR")
                        {
                            arm.Move_Body_LR(step, speed);
                            Log("颈部左右移动" + "步长" + step.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控颈部左右移动";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "FN")
                        {
                            arm.Move_Body_FN(step, speed);
                            Log("颈部前后移动" + "步长" + step.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控颈部前后移动";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HUD")
                        {
                            arm.Move_Head_UD(step, speed);
                            Log("头部上下移动" + "步长" + step.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部上下移动";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HLR")
                        {
                            arm.Move_Head_LR(step, speed);
                            Log("头部左右移动" + "步长" + step.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部左右移动";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HSLR")
                        {
                            arm.Move_Head_SLR(step, speed);
                            Log("头部左右歪斜" + "步长" + step.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部左右歪斜";
                            Application.DoEvents();
                        }
                    }
                    if (cmd[0] == "SET")
                    {
                        double pos = Convert.ToDouble(cmd[2]);
                        double speed = 0;
                        try
                        {
                             speed = Convert.ToDouble(cmd[3]);
                        }
                        catch
                        { }
                        if (cmd[1] == "UD")
                        {
                            arm.Set_Body_UD(pos, speed);
                            Log("颈部上下移动" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控颈部上下位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "LR")
                        {
                            arm.Set_Body_LR(pos, speed);
                            Log("颈部左右移动" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控颈部左右位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "FN")
                        {
                            arm.Set_Body_FN(pos, speed);
                            Log("颈部前后移动" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控颈部前后位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HUD")
                        {
                            arm.Set_Head_UD(pos, speed);
                            Log("头部上下移动" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部上下位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HLR")
                        {
                            arm.Set_Head_LR(pos, speed);
                            Log("头部左右移动" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部左右位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "HSLR")
                        {
                            arm.Set_Head_SLR(pos, speed);
                            Log("头部左右歪斜" + "位置" + pos.ToString() + " 速度" + speed.ToString());
                            lab_status.Text = "遥控头部左右歪斜位置";
                            Application.DoEvents();
                        }
                        if (cmd[1] == "LCD")
                        {
                            arm.Set_LCD(14,(int)pos);
                            Log("LCD" + "状态" + pos.ToString());
                            lab_status.Text = "遥控LCD状态";
                            Application.DoEvents();
                        }
                    }
                }

            }
            catch
            { }
        }

        private void btn_Neck_Down_Click(object sender, EventArgs e)
        {

        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            tmr_Report.Enabled = false;
            try
            {
                List<string> report = new List<string>();
                report.Add("");
                report.Add("");
                report.Add("");
                if (lab_status.Text.Contains("自动运行"))
                {
                    report[0] = ("自动");
                }
                else 
                {
                    report[0] = ("手动");
                }
                if (lab_movement.Text.Contains("移动中"))
                {
                    report[1] = ("移动中");
                }else
                {                  
                    report[1] = ("静止");
                    
                }
                report[2] = reportno.ToString();
                reportno++;
                file.WriteToFile(report, "MotoReport.ini");
            }
            catch { }
            tmr_Report.Enabled = true;
        }

        private void tmr_Cmd_Tick(object sender, EventArgs e)
        {
            btn_GetCmd_Click(sender, e);
        }

        private void tmr_Report_Tick(object sender, EventArgs e)
        {
            btn_Report_Click(sender, e);
        }

        private void btn_LCDOff_Click(object sender, EventArgs e)
        {
            
            arm.Set_LCD(14, 0);
        }

        private void btn_LCDOn_Click(object sender, EventArgs e)
        {
            arm.Set_LCD(14,1);
        }

        private void btn_GetBias_Click(object sender, EventArgs e)
        {
            //////////////数据格式
            // 左右偏移 0
            // 上下偏移 1
            // 远近距离 2
            // 人名 3
            // 是否找到人脸4 
            // 序列号

            try
            {
                FacePos = file.ReadToList("Facepos.ini");
                long l_currFaceDetection = long.Parse(FacePos[5].ToString());

                if (l_LastFaceDetection != l_currFaceDetection)
                {
                    i_missFaceDataCount = 0;
                }else
                {
                    i_missFaceDataCount++;
                }

                    
                if (((FacePos[4].ToString() == "Face Found")&&(l_LastFaceDetection != l_currFaceDetection))||(i_missFaceDataCount<3))
                {

                    L = int.Parse(FacePos[0].ToString());
                    H = int.Parse(FacePos[1].ToString());
                    Distance = int.Parse(FacePos[2].ToString());
                    l_LastFaceDetection = l_currFaceDetection;
                    L =  (int)(((L - 155))*0.8);
                    H = (-1) * (int)((H - 125) * 1.3);
                    Distance = Distance - 76;

                    txt_LR_Bias.Text = L.ToString();
                    txt_UD_Bias.Text = H.ToString();
                    txt_FN_Bias.Text = Distance.ToString();

                    txt_FaceLocked.Text = FacePos[3];
                }
                else
                {
                    L = 999;H = 999;Distance = 999;
                }

            }
            catch
            { }
        }
        string s_lastLog = "";
        private void Log(string txt)
        {
            if (txt != s_lastLog)
            {
                rtb_Output.Text = rtb_Output.Text + txt + "\n";
                rtb_Output.SelectionStart = rtb_Output.Text.Length;
                rtb_Output.ScrollToCaret();
                s_lastLog = txt;
                if (rtb_Output.Text.Length > 500)
                {
                    rtb_Output.Text = rtb_Output.Text.Substring(rtb_Output.Text.Length - 500, 500);
                }
            }
        }
    }
}
