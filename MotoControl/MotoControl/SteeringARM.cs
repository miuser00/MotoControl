using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MotorControl
{

    public class Joint_Setup
    {
        //关节角度极限
        public double MinAngle = 0;
        public double MaxAngle = 0;
        public void Save(string shortfilename)
        {
            FileWR file = new FileWR();
            List<string> list = new List<string>();
            list.Add(MinAngle.ToString());
            list.Add(MaxAngle.ToString());
            file.WriteToFile(list, Application.StartupPath + "\\" + shortfilename);
        }
        public void Load(string shortfilename)
        {
            FileWR file = new FileWR();
            List<string> list = file.ReadToList(Application.StartupPath + "\\" + shortfilename);
            MinAngle = Convert.ToDouble(list[0]);
            MaxAngle = Convert.ToDouble(list[1]);
        }
    }
    public class Joint_Status
    {
        //关节当前状态
        public double Current = 0;
        public double Speed = 0;

    }

    //机械臂动作
    public class Arm_Setup
    {
        //极限位置
        public readonly double LowLimit = -100; //lowest
        public readonly double HiLimit = 100;   //highest
        //默认位置
        public double DefaultPos = 0;    //-100 ~ 100
        public void Save(string shortfilename)
        {
            FileWR file = new FileWR();
            List<string> list = new List<string>();
            list.Add(DefaultPos.ToString());
            file.WriteToFile(list, Application.StartupPath + "\\" + shortfilename);
        }
        public void Load(string shortfilename)
        {
            FileWR file = new FileWR();
            List<string> list = file.ReadToList(Application.StartupPath + "\\" + shortfilename);
            DefaultPos = Convert.ToDouble(list[0]);
        }
    }
    public class Arm_Status
    { 
        //当前 位置，速度
        public double pos = 0;
        public double speed = 5;  //1-10

    }

    //机械臂分解关节姿态

    public class ARM6
    {
        public string Profilename = "default";
        //姿态 （输入）
        public Arm_Status Head_LR=new Arm_Status(); //头部左转右转
        public Arm_Status Head_UD = new Arm_Status(); //低头抬头
        public Arm_Status Head_SLR = new Arm_Status(); //左歪右歪

        public Arm_Status Body_LR = new Arm_Status(); //左转右转
        public Arm_Status Body_UD = new Arm_Status(); //上下
        public Arm_Status Body_FN = new Arm_Status(); //前后

        public Arm_Setup s_Head_LR=new Arm_Setup(); //头部左转右转
        public Arm_Setup s_Head_UD = new Arm_Setup(); //低头抬头
        public Arm_Setup s_Head_SLR = new Arm_Setup(); //左歪右歪

        public Arm_Setup s_Body_LR = new Arm_Setup(); //左转右转
        public Arm_Setup s_Body_UD = new Arm_Setup(); //上下
        public Arm_Setup s_Body_FN = new Arm_Setup(); //前后

        //舵机控制 （输出）
        public Joint_Status[] JointStatus = new Joint_Status[6];
        public SteeringControl[] Joints = new SteeringControl[6];
        public SteeringControl LCD;

        //舵机设置
        public Joint_Setup[] JointSetups = new Joint_Setup[6];

        public ARM6(string profilename, SteeringControl.SendMSG messagersender=null)
        {
            Profilename = profilename;
            Joints[0] = new SteeringControl((byte)4);
            Joints[1] = new SteeringControl((byte)3);
            Joints[2] = new SteeringControl((byte)5);
            Joints[3] = new SteeringControl((byte)0);
            Joints[4] = new SteeringControl((byte)1);
            Joints[5] = new SteeringControl((byte)2);

            LCD = new SteeringControl((byte)14);

            for (int i = 0; i < 6; i++)
            {
                Joints[i].SendMessage = messagersender;
            }

            LCD.SendMessage = messagersender;

            for (int i = 0; i < 6; i++)
            {
                JointSetups[i] = new Joint_Setup();
                JointStatus[i] = new Joint_Status();
            }


            try
            {
                for (int i = 0; i < 6; i++)
                {
                    JointSetups[i].Load(profilename + "_" + "joint" + i.ToString() + ".cfg");

                }
                s_Head_LR.Load(profilename + "_" + "Head_LR.cfg");
                s_Head_UD.Load(profilename + "_" + "Head_UD.cfg");
                s_Head_SLR.Load(profilename + "_" + "Head_SLR.cfg");
                s_Body_LR.Load(profilename + "_" + "Body_LR.cfg");
                s_Body_UD.Load(profilename + "_" + "Body_UD.cfg");
                s_Body_FN.Load(profilename + "_" + "Body_FN.cfg");
            }
            catch
            {
                MessageBox.Show("读取配置文件失败");
            }
           
        }

        //设置关节的极限位置和默认位置
        public void SetPosAsJointLowLimit(int channel)
        {
            JointSetups[channel].MinAngle = JointStatus[channel].Current;
        }
        public void SetPosAsJointHighLimit(int channel)
        {
            JointSetups[channel].MaxAngle = JointStatus[channel].Current;
        }

        public void SetPosAsDefault()
        {
            s_Head_LR.DefaultPos = Head_LR.pos;
            s_Head_UD.DefaultPos = Head_UD.pos;
            s_Head_SLR.DefaultPos = Head_SLR.pos;
            s_Body_LR.DefaultPos = Body_LR.pos;
            s_Body_UD.DefaultPos = Body_UD.pos;
            s_Body_FN.DefaultPos = Body_FN.pos;
        }

        public void Init (string initfile)
        {
            Set_Head_LR(s_Head_LR.DefaultPos, 10);
            Set_Head_UD(s_Head_UD.DefaultPos, 10);
            Set_Head_SLR(s_Head_SLR.DefaultPos, 10);

            Set_Body_LR(s_Body_LR.DefaultPos, 10);
            Set_Body_UD(s_Body_UD.DefaultPos, 10);
            Set_Body_FN(s_Body_FN.DefaultPos, 10);
        }
        public void Save(int channel=-1)
        {
            SaveSetting(Profilename,channel);
        }
        public void SaveSetting(string profilename,int channel)
        {
            if (channel == -1)
            {
                for (int i = 0; i < 6; i++)
                {
                    JointSetups[i].Save(profilename + "_" + "joint" + i.ToString() + ".cfg");
                }
            }else
            if (channel == -2)
            {
                s_Head_LR.Save(profilename + "_" + "Head_LR.cfg");
                s_Head_UD.Save(profilename + "_" + "Head_UD.cfg");
                s_Head_SLR.Save(profilename + "_" + "Head_SLR.cfg");
                s_Body_LR.Save(profilename + "_" + "Body_LR.cfg");
                s_Body_UD.Save(profilename + "_" + "Body_UD.cfg");
                s_Body_FN.Save(profilename + "_" + "Body_FN.cfg");
            }
                else
            {
                JointSetups[channel].Save(profilename + "_" + "joint" + channel.ToString() + ".cfg");
            }
        }
        public void Set_Joint_Raw(int channel, double angle, double speed)
        {
            //保存手臂状态
            JointStatus[channel].Current = angle;
            JointStatus[channel].Speed = speed;
            Joints[channel].SetAngle(angle);
            Joints[channel].SetSpeed((int)speed);
            Joints[channel].Send();
            Joints[channel].SendMessage(1, (byte)channel,(int)speed);


        }
        public void Set_Head_LR(double pos, double speed)
        {
            //保存手臂状态
            Head_LR.pos = pos;
            Head_LR.speed = speed;

            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[0].MaxAngle - JointSetups[0].MinAngle) / (s_Head_LR.HiLimit - s_Head_LR.LowLimit);
            double d_MiddleValue = (JointSetups[0].MaxAngle + JointSetups[0].MinAngle) / 2;
            JointStatus[0].Current = d_MiddleValue + Head_LR.pos * d_linnerfactor;
            JointStatus[0].Speed = speed;
            Joints[0].SetAngle(JointStatus[0].Current);
            Joints[0].SetSpeed((int)JointStatus[0].Speed);
            Joints[0].Send();
            Joints[0].SendMessage(1, (byte)0, (int)speed);

        }



        public void Set_Head_UD(double pos, double speed)
        {
            //保存手臂状态
            Head_UD.pos = pos;
            Head_UD.speed = speed;

            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[1].MaxAngle - JointSetups[1].MinAngle) / (s_Head_UD.HiLimit - s_Head_UD.LowLimit);
            double d_MiddleValue = (JointSetups[1].MaxAngle + JointSetups[1].MinAngle) / 2;
            JointStatus[1].Current = d_MiddleValue + Head_UD.pos * d_linnerfactor;
            JointStatus[1].Speed = speed;
            Joints[1].SetAngle(JointStatus[1].Current);
            Joints[1].SetSpeed((int)JointStatus[1].Speed);
            Joints[1].SendMessage(1, (byte)Joints[1].Channel, (int)speed);
            Joints[1].Send();

        }

        public void Set_Head_SLR(double pos, double speed)
        {
            //保存手臂状态
            Head_SLR.pos = pos;
            Head_SLR.speed = speed;
            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[2].MaxAngle - JointSetups[2].MinAngle) / (s_Head_SLR.HiLimit - s_Head_SLR.LowLimit);
            double d_MiddleValue = (JointSetups[2].MaxAngle + JointSetups[2].MinAngle) / 2;
            JointStatus[2].Current = d_MiddleValue + Head_SLR.pos * d_linnerfactor;
            JointStatus[2].Speed = speed;
            Joints[2].SetAngle(JointStatus[2].Current);
            Joints[2].SetSpeed((int)JointStatus[2].Speed);
            Joints[2].SendMessage(1, (byte)Joints[2].Channel, (int)speed);
            Joints[2].Send();
        }

        public void Set_Body_LR(double pos, double speed)
        {
            //保存手臂状态
            Body_LR.pos = pos;
            Body_LR.speed = speed;
            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[3].MaxAngle - JointSetups[3].MinAngle) / (s_Body_LR.HiLimit - s_Body_LR.LowLimit);
            double d_MiddleValue = (JointSetups[3].MaxAngle + JointSetups[3].MinAngle) / 2;
            JointStatus[3].Current = d_MiddleValue + Body_LR.pos * d_linnerfactor;
            JointStatus[3].Speed = speed;
            Joints[3].SetAngle(JointStatus[3].Current);
            Joints[3].SetSpeed((int)JointStatus[3].Speed);
            Joints[3].SendMessage(1, (byte)Joints[3].Channel, (int)speed);
            Joints[3].Send();
        }

        public void Set_Body_UD(double pos, double speed)
        {
            //保存手臂状态
            Body_UD.pos = pos;
            Body_UD.speed = speed;
            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[4].MaxAngle - JointSetups[4].MinAngle) / (s_Body_UD.HiLimit - s_Body_UD.LowLimit);
            double d_MiddleValue = (JointSetups[4].MaxAngle + JointSetups[4].MinAngle) / 2;
            JointStatus[4].Current = d_MiddleValue + Body_UD.pos * d_linnerfactor;
            JointStatus[4].Speed = speed;
            Joints[4].SetAngle(JointStatus[4].Current);
            Joints[4].SetSpeed((int)JointStatus[4].Speed);
            Joints[4].SendMessage(1, (byte)Joints[4].Channel, (int)speed);
            Joints[4].Send();
        }

        public void Set_Body_FN(double pos, double speed)
        {
            //保存手臂状态
            Body_FN.pos = pos;
            Body_FN.speed = speed;
            //变换为关节状态
            //线性变换
            double d_linnerfactor = (JointSetups[5].MaxAngle - JointSetups[5].MinAngle) / (s_Body_FN.HiLimit - s_Body_FN.LowLimit);
            double d_MiddleValue = (JointSetups[5].MaxAngle + JointSetups[5].MinAngle) / 2;
            JointStatus[5].Current = d_MiddleValue + Body_FN.pos * d_linnerfactor;
            JointStatus[5].Speed = speed;
            Joints[5].SetAngle(JointStatus[5].Current);
            Joints[5].SetSpeed((int)JointStatus[5].Speed);
            Joints[5].SendMessage(1, (byte)Joints[5].Channel, (int)speed);
            Joints[5].Send();
        }

        public void Set_LCD(int channel=14,int status=1)
        {
            if (status == 1) status = 2500;
            LCD.SendMessage(1, (byte)channel, 20);
            LCD.SendMessage(2, (byte)channel, status);
        }
        public void Move_Head_LR(double step, double speed)
        {
            if (((Head_LR.pos+step)>=s_Head_LR.LowLimit)&&((Head_LR.pos+step)<=s_Head_LR.HiLimit))
                Set_Head_LR(Head_LR.pos + step, speed);
        }
        public void Move_Head_UD(double step, double speed)
        {
            if (((Head_UD.pos + step) >= s_Head_UD.LowLimit) && ((Head_UD.pos + step) <= s_Head_UD.HiLimit))
                Set_Head_UD(Head_UD.pos + step, speed);
        }
        public void Move_Head_SLR(double step, double speed)
        {
            if (((Head_SLR.pos + step) >= s_Head_SLR.LowLimit) && ((Head_SLR.pos + step) <= s_Head_SLR.HiLimit))
                Set_Head_SLR(Head_SLR.pos + step, speed);
        }
        public void Move_Body_LR(double step, double speed)
        {
            if (((Body_LR.pos + step) >= s_Body_LR.LowLimit) && ((Body_LR.pos + step) <= s_Body_LR.HiLimit))
                Set_Body_LR(Body_LR.pos + step, speed);
        }
        public void Move_Body_UD(double step, double speed)
        {
            if (((Body_UD.pos + step) >= s_Body_UD.LowLimit) && ((Body_UD.pos + step) <= s_Body_UD.HiLimit))
                Set_Body_UD(Body_UD.pos + step, speed);
        }
        public void Move_Body_FN(double step, double speed)
        {
            if (((Body_FN.pos + step) >= s_Body_FN.LowLimit) && ((Body_FN.pos + step) <= s_Body_FN.HiLimit))
                Set_Body_FN(Body_FN.pos + step, speed);
        }
    }
}