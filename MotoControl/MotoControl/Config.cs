using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MotorControl
{
    public class Config : Form
    {
        public delegate void Send(byte cmd, byte ch, int data);

        public Config.Send SendMessage;

        private IContainer components;

        private GroupBox groupBox2;

        private Button button4;

        private TextBox textBoxMem;

        private CheckBox checkBox1;

        private ListBox listBox2;

        private Label label5;

        private Label label2;

        private Label label4;

        private TextBox textBox3;

        private Timer timer1;

        public Config(Config.Send SendHandle)
        {
            this.InitializeComponent();
            this.SendMessage = SendHandle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.SendMessage(14, 0, (int)Convert.ToUInt16(this.textBoxMem.Text));
            }
            catch (Exception)
            {
            }
        }

        public void DecodeData(byte[] byteTemp)
        {
            short num = BitConverter.ToInt16(byteTemp, 3);
            if (this.checkBox1.Checked)
            {
                this.textBox3.Text = "0x" + num.ToString("x");
                return;
            }
            this.textBox3.Text = num.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedIndex < 0)
            {
                return;
            }
            this.SendMessage(0, (byte)this.listBox2.SelectedIndex, 0);
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
            this.components = new Container();
            this.groupBox2 = new GroupBox();
            this.button4 = new Button();
            this.textBoxMem = new TextBox();
            this.checkBox1 = new CheckBox();
            this.listBox2 = new ListBox();
            this.label5 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.textBox3 = new TextBox();
            this.timer1 = new Timer(this.components);
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBoxMem);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Location = new Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(274, 243);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "状态";
            this.button4.Location = new Point(145, 199);
            this.button4.Name = "button4";
            this.button4.Size = new Size(56, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "设置";
            this.button4.UseVisualStyleBackColor = true;
            this.textBoxMem.Location = new Point(145, 172);
            this.textBoxMem.Name = "textBoxMem";
            this.textBoxMem.Size = new Size(74, 21);
            this.textBoxMem.TabIndex = 1;
            this.textBoxMem.Text = "0";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(148, 91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(66, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "HEX显示";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Items.AddRange(new object[]
            {
                "ACT0 \t\t\t\t\t",
                "ACT1 \t\t\t\t\t",
                "ACT2 \t\t\t\t\t",
                "ACT3\t\t\t\t\t",
                "ACT4 \t\t\t\t\t",
                "ACT5\t\t\t\t\t",
                "ACT6\t\t\t\t\t",
                "ACT7\t\t\t\t\t",
                "ACT8\t\t\t\t\t",
                "ACT9\t\t\t\t\t",
                "ACT10\t\t\t\t\t",
                "ACT11\t\t\t\t\t",
                "ACT12\t\t\t\t\t",
                "ACT13\t\t\t\t\t",
                "ACT14\t\t\t\t\t",
                "ACT15\t\t\t\t\t",
                "ACT16\t\t\t\t\t",
                "ACTGP\t\t\t\t\t",
                "ID\t\t\t\t\t\t",
                "BAUD\t\t\t\t\t",
                "IICADDR\t\t\t\t",
                "STOP\t\t\t\t\t",
                "ENCODE0\t\t\t\t",
                "ENCODE1\t\t\t\t",
                "AD0\t\t\t\t\t\t",
                "AD1\t\t\t\t\t\t",
                "AD2\t\t\t\t\t\t",
                "AD3\t\t\t\t\t\t",
                "AD4\t\t\t\t\t\t",
                "AD5\t\t\t\t\t\t",
                "AD6\t\t\t\t\t\t",
                "AD7",
                "PROTECT",
                "DC0\t\t\t\t\t\t",
                "DC1\t\t\t\t\t\t",
                "DC2\t\t\t\t\t\t",
                "DC3\t\t\t\t\t\t",
                "STEP0\t\t\t\t\t",
                "STEP1\t\t\t\t\t",
                "STEP2\t\t\t\t\t",
                "STEP3\t\t\t\t\t",
                "STEPSPEED0\t\t",
                "STEPSPEED1\t\t",
                "STEPSPEED2\t\t",
                "STEPSPEED3\t\t",
                "STEERING0\t\t\t",
                "STEERING1\t\t\t",
                "STEERING2\t\t\t",
                "STEERING3 \t\t",
                "STEERING4\t\t\t",
                "STEERING5\t\t\t",
                "STEERING6\t\t\t",
                "STEERING7\t\t\t",
                "STEERING8\t\t\t",
                "STEERING9\t\t\t",
                "STEERING10\t\t",
                "STEERING11\t\t",
                "STEERING12\t\t",
                "STEERING13\t\t",
                "STEERING14\t\t",
                "STEERING15\t\t",
                "STEERING16\t\t",
                "STEERING17\t\t",
                "STEERING18\t\t",
                "STEERING19\t\t",
                "STEERING20\t\t",
                "STEERING21\t\t",
                "STEERING22\t\t",
                "STEERING23\t\t",
                "STEERING24\t\t",
                "STEERING25\t\t",
                "STEERING26\t\t",
                "STEERING27\t\t",
                "STEERING28\t\t",
                "STEERING29\t\t",
                "STEERING30\t\t",
                "STEERING31\t\t",
                "STEERINGSPEED0\t",
                "STEERINGSPEED1\t",
                "STEERINGSPEED2\t",
                "STEERINGSPEED3 ",
                "STEERINGSPEED4\t",
                "STEERINGSPEED5\t",
                "STEERINGSPEED6\t",
                "STEERINGSPEED7\t",
                "STEERINGSPEED8\t",
                "STEERINGSPEED9\t",
                "STEERINGSPEED10",
                "STEERINGSPEED11",
                "STEERINGSPEED12",
                "STEERINGSPEED13",
                "STEERINGSPEED14",
                "STEERINGSPEED15",
                "STEERINGSPEED16",
                "STEERINGSPEED17",
                "STEERINGSPEED18",
                "STEERINGSPEED19",
                "STEERINGSPEED20",
                "STEERINGSPEED21",
                "STEERINGSPEED22",
                "STEERINGSPEED23",
                "STEERINGSPEED24",
                "STEERINGSPEED25",
                "STEERINGSPEED26",
                "STEERINGSPEED27",
                "STEERINGSPEED28",
                "STEERINGSPEED29",
                "STEERINGSPEED30",
                "STEERINGSPEED31",
                "ACTINDEX",
                "MEMREAD",
                "MEMCMD",
                "MEMCH",
                "MEMDATA"
            });
            this.listBox2.Location = new Point(6, 41);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(116, 172);
            this.listBox2.TabIndex = 2;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(146, 24);
            this.label5.Name = "label5";
            this.label5.Size = new Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "数值：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(142, 154);
            this.label2.Name = "label2";
            this.label2.Size = new Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "内存读取地址：";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "寄存器：";
            this.textBox3.BackColor = SystemColors.ControlLightLight;
            this.textBox3.Font = new Font("Times New Roman", 21.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(144, 43);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(102, 41);
            this.textBox3.TabIndex = 1;
            this.textBox3.TextAlign = HorizontalAlignment.Right;
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(298, 264);
            base.Controls.Add(this.groupBox2);
            base.Name = "Config";
            this.Text = "Config";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}
