using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MotorControl
{
    public class SteeringControl : UserControl
    {
        private enum EnumMousePointPosition
        {
            MouseSizeNone,
            MouseSizeRight,
            MouseSizeLeft,
            MouseSizeBottom,
            MouseSizeTop,
            MouseSizeTopLeft,
            MouseSizeTopRight,
            MouseSizeBottomLeft,
            MouseSizeBottomRight,
            MouseDrag
        }

        public delegate void SendMSG(byte cmd, byte ch, int data);

        private const int Band = 5;

        private const int MinWidth = 10;

        private const int MinHeight = 10;

        private SteeringControl.EnumMousePointPosition m_MousePointPosition;

        private Point p;

        private Point p1;

        public byte Channel;

        public SteeringControl.SendMSG SendMessage;

        public byte UseUs = 1;

        public double Angle;

        public int iAngleUs;

        public int iSpeed;

        public int iMaxPWM = 2500;

        public int iMinPWM = 500;

        private IContainer components;

        private TrackBar trackBar1;

        private TextBox textBox1;

        private Label label3;

        private Label label4;

        private GroupBox groupBox1;

        private Label label1;

        private ComboBox comboBox1;

        private void MyMouseDown(object sender, MouseEventArgs e)
        {
            this.p.X = e.X;
            this.p.Y = e.Y;
            this.p1.X = e.X;
            this.p1.Y = e.Y;
        }

        private void MyMouseLeave(object sender, EventArgs e)
        {
            this.m_MousePointPosition = SteeringControl.EnumMousePointPosition.MouseSizeNone;
            this.Cursor = Cursors.Arrow;
        }

        private SteeringControl.EnumMousePointPosition MousePointPosition(Size size, MouseEventArgs e)
        {
            if (e.X >= 0 | e.X <= size.Width | e.Y >= 0 | e.Y <= size.Height)
            {
                return SteeringControl.EnumMousePointPosition.MouseDrag;
            }
            return SteeringControl.EnumMousePointPosition.MouseSizeNone;
        }

        private void MyMouseMove(object sender, MouseEventArgs e)
        {
            Control parent = (sender as Control).Parent;
            if (e.Button == MouseButtons.Left)
            {
                switch (this.m_MousePointPosition)
                {
                    case SteeringControl.EnumMousePointPosition.MouseSizeRight:
                        parent.Width = parent.Width + e.X - this.p1.X;
                        this.p1.X = e.X;
                        this.p1.Y = e.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeLeft:
                        parent.Left = parent.Left + e.X - this.p.X;
                        parent.Width -= e.X - this.p.X;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottom:
                        parent.Height = parent.Height + e.Y - this.p1.Y;
                        this.p1.X = e.X;
                        this.p1.Y = e.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTop:
                        parent.Top += e.Y - this.p.Y;
                        parent.Height -= e.Y - this.p.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTopLeft:
                        parent.Left = parent.Left + e.X - this.p.X;
                        parent.Top += e.Y - this.p.Y;
                        parent.Width -= e.X - this.p.X;
                        parent.Height -= e.Y - this.p.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTopRight:
                        parent.Top += e.Y - this.p.Y;
                        parent.Width += e.X - this.p1.X;
                        parent.Height -= e.Y - this.p.Y;
                        this.p1.X = e.X;
                        this.p1.Y = e.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottomLeft:
                        parent.Left = parent.Left + e.X - this.p.X;
                        parent.Width -= e.X - this.p.X;
                        parent.Height = parent.Height + e.Y - this.p1.Y;
                        this.p1.X = e.X;
                        this.p1.Y = e.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottomRight:
                        parent.Width = parent.Width + e.X - this.p1.X;
                        parent.Height = parent.Height + e.Y - this.p1.Y;
                        this.p1.X = e.X;
                        this.p1.Y = e.Y;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseDrag:
                        parent.Left = parent.Left + e.X - this.p.X;
                        parent.Top = parent.Top + e.Y - this.p.Y;
                        break;
                }
                if (parent.Width < 10)
                {
                    parent.Width = 10;
                }
                if (parent.Height < 10)
                {
                    parent.Height = 10;
                    return;
                }
            }
            else
            {
                this.m_MousePointPosition = this.MousePointPosition(parent.Size, e);
                switch (this.m_MousePointPosition)
                {
                    case SteeringControl.EnumMousePointPosition.MouseSizeNone:
                        this.Cursor = Cursors.Arrow;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeRight:
                        this.Cursor = Cursors.SizeWE;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeLeft:
                        this.Cursor = Cursors.SizeWE;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottom:
                        this.Cursor = Cursors.SizeNS;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTop:
                        this.Cursor = Cursors.SizeNS;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTopLeft:
                        this.Cursor = Cursors.SizeNWSE;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeTopRight:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottomLeft:
                        this.Cursor = Cursors.SizeNESW;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseSizeBottomRight:
                        this.Cursor = Cursors.SizeNWSE;
                        return;
                    case SteeringControl.EnumMousePointPosition.MouseDrag:
                        this.Cursor = Cursors.SizeAll;
                        return;
                    default:
                        return;
                }
            }
        }

        public SteeringControl(byte byteChannel)
        {
            this.InitializeComponent();
            this.Channel = byteChannel;
            this.groupBox1.Text = "M" + byteChannel.ToString();
            for (int i = 1; i < 21; i++)
            {
                this.comboBox1.Items.Add((9 * i).ToString() + "°/s");
            }
            this.comboBox1.SelectedIndex = 19;
            //this.SetAngle(90.0);
            this.iSpeed = this.comboBox1.SelectedIndex;
            this.groupBox1.MouseDown += new MouseEventHandler(this.MyMouseDown);
            this.groupBox1.MouseLeave += new EventHandler(this.MyMouseLeave);
            this.groupBox1.MouseMove += new MouseEventHandler(this.MyMouseMove);
        }

        public void SetAngle(double AngleInput)
        {
            this.Angle = AngleInput;
            this.iAngleUs = (int)((double)this.iMinPWM + this.Angle / 180.0 * (double)(this.iMaxPWM - this.iMinPWM));
            this.trackBar1.Value = (int)this.Angle;
            this.textBox1.Text = this.Angle.ToString("F0");

        }
        public int GetAngle()
        {
            return int.Parse(textBox1.Text);
        }
        public void SetSpeed(int SpeedInput)
        {
            if (SpeedInput > 0 && SpeedInput < 20)
            {
                this.iSpeed = SpeedInput;
                this.comboBox1.SelectedIndex = this.iSpeed;
                //SendMessage(1, Channel, (int)SpeedInput);
            }
        }

        public short GetPWMFromAngle(double AngleInput)
        {
            return (short)((double)this.iMinPWM + AngleInput / 180.0 * (double)(this.iMaxPWM - this.iMinPWM));
        }

        public void SetPWMRange(int iMin, int iMax)
        {
            this.iMaxPWM = iMax;
            this.iMinPWM = iMin;
        }

        public void Send()
        {
           short data = (short)this.iAngleUs;
            this.SendMessage(2, this.Channel, (int)data);
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetAngle((double)this.trackBar1.Value);
            this.Send();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SetAngle(double.Parse(this.textBox1.Text));
                this.Send();
            }
            catch (Exception)
            {
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.textBox1.Text = this.trackBar1.Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetSpeed(this.comboBox1.SelectedIndex);
                this.SendMessage(1, this.Channel, this.iSpeed + 1);
            }
            catch (Exception)
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SteeringControl));
            this.trackBar1 = new TrackBar();
            this.textBox1 = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.comboBox1 = new ComboBox();
            this.label1 = new Label();
            ((ISupportInitialize)this.trackBar1).BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            componentResourceManager.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.BackColor = SystemColors.ButtonFace;
            this.trackBar1.Maximum = 180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickStyle = TickStyle.None;
            this.trackBar1.Value = 90;
            this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
            this.trackBar1.MouseUp += new MouseEventHandler(this.trackBar1_MouseUp);
            componentResourceManager.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            componentResourceManager.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            componentResourceManager.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            componentResourceManager.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            componentResourceManager.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Name = "SteeringControl";
            ((ISupportInitialize)this.trackBar1).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}
