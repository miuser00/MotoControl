namespace MotorControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pan_Control = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.spSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.Status1 = new System.Windows.Forms.Label();
            this.usb = new UsbLibrary.UsbHidPort(this.components);
            this.tmr_USBConn = new System.Windows.Forms.Timer(this.components);
            this.textBoxBoard = new System.Windows.Forms.Label();
            this.btn_Watch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lab_movement = new System.Windows.Forms.Label();
            this.lab_status = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_MANUAL_ARM = new System.Windows.Forms.Button();
            this.btn_ResetARM = new System.Windows.Forms.Button();
            this.btn_Head_LS = new System.Windows.Forms.Button();
            this.btn_Head_RS = new System.Windows.Forms.Button();
            this.btn_Head_RT = new System.Windows.Forms.Button();
            this.btn_Head_UP = new System.Windows.Forms.Button();
            this.btn_Head_LT = new System.Windows.Forms.Button();
            this.btn_Head_Down = new System.Windows.Forms.Button();
            this.btn_Neck_FT = new System.Windows.Forms.Button();
            this.btn_Neck_BH = new System.Windows.Forms.Button();
            this.btn_Neck_RT = new System.Windows.Forms.Button();
            this.btn_Neck_UP = new System.Windows.Forms.Button();
            this.btn_AUTO_ARM = new System.Windows.Forms.Button();
            this.btn_Neck_LT = new System.Windows.Forms.Button();
            this.btn_Neck_Down = new System.Windows.Forms.Button();
            this.grp_FaceSearch = new System.Windows.Forms.GroupBox();
            this.pan_FaceSearch = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txt_FaceCentral = new System.Windows.Forms.TextBox();
            this.txt_LR_Bias = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txt_UD_Bias = new System.Windows.Forms.TextBox();
            this.txt_FaceLocked = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_FN_Bias = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_Neck_LR = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_Head_LR = new System.Windows.Forms.TextBox();
            this.btn_FarPilot = new System.Windows.Forms.Button();
            this.btn_NearPilot = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_Report = new System.Windows.Forms.Button();
            this.btn_GetCmd = new System.Windows.Forms.Button();
            this.rtb_Output = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tb_NeckSpeed = new System.Windows.Forms.TrackBar();
            this.tbHeadSpeed = new System.Windows.Forms.TrackBar();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txt_Head_SLR_Speed = new System.Windows.Forms.TextBox();
            this.txt_Neck_FN_Speed = new System.Windows.Forms.TextBox();
            this.txt_Head_SLR = new System.Windows.Forms.TextBox();
            this.txt_Neck_FN = new System.Windows.Forms.TextBox();
            this.txt_Head_UD_Speed = new System.Windows.Forms.TextBox();
            this.txt_Neck_UD_Speed = new System.Windows.Forms.TextBox();
            this.txt_Head_UD = new System.Windows.Forms.TextBox();
            this.txt_Neck_UD = new System.Windows.Forms.TextBox();
            this.txt_Head_LR_Speed = new System.Windows.Forms.TextBox();
            this.txt_Neck_LR_Speed = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btn_SaveAllChannel = new System.Windows.Forms.Button();
            this.btn_SaveDefaultPos = new System.Windows.Forms.Button();
            this.btn_SaveChannel = new System.Windows.Forms.Button();
            this.trb_Calc = new System.Windows.Forms.TrackBar();
            this.rad_HUD = new System.Windows.Forms.RadioButton();
            this.rad_NFN = new System.Windows.Forms.RadioButton();
            this.btn_SetDefaultPos = new System.Windows.Forms.Button();
            this.rad_NUD = new System.Windows.Forms.RadioButton();
            this.rad_NLR = new System.Windows.Forms.RadioButton();
            this.rad_HTLR = new System.Windows.Forms.RadioButton();
            this.rad_HLR = new System.Windows.Forms.RadioButton();
            this.btn_SetLowLimit = new System.Windows.Forms.Button();
            this.btn_SetHighLimit = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.tmr_NearPilot = new System.Windows.Forms.Timer(this.components);
            this.tmr_FarPilot = new System.Windows.Forms.Timer(this.components);
            this.tmr_RefreshARMStatus = new System.Windows.Forms.Timer(this.components);
            this.tmr_RefreshFaceStatus = new System.Windows.Forms.Timer(this.components);
            this.btn_FaceFollow = new System.Windows.Forms.Button();
            this.tmr_FaceFollow = new System.Windows.Forms.Timer(this.components);
            this.tmr_Watch = new System.Windows.Forms.Timer(this.components);
            this.tmr_Run = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tmr_Cmd = new System.Windows.Forms.Timer(this.components);
            this.tmr_Report = new System.Windows.Forms.Timer(this.components);
            this.btn_LCDOn = new System.Windows.Forms.Button();
            this.btn_LCDOff = new System.Windows.Forms.Button();
            this.pan_Control.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grp_FaceSearch.SuspendLayout();
            this.pan_FaceSearch.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_NeckSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadSpeed)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Calc)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_Control
            // 
            this.pan_Control.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan_Control.Controls.Add(this.label18);
            this.pan_Control.Controls.Add(this.label17);
            this.pan_Control.Controls.Add(this.label16);
            this.pan_Control.Controls.Add(this.label15);
            this.pan_Control.Controls.Add(this.label14);
            this.pan_Control.Controls.Add(this.label13);
            resources.ApplyResources(this.pan_Control, "pan_Control");
            this.pan_Control.Name = "pan_Control";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // Status1
            // 
            resources.ApplyResources(this.Status1, "Status1");
            this.Status1.ForeColor = System.Drawing.Color.White;
            this.Status1.Name = "Status1";
            // 
            // usb
            // 
            this.usb.ProductId = 256;
            this.usb.VendorId = 6432;
            this.usb.OnSpecifiedDeviceArrived += new System.EventHandler(this.usb_OnSpecifiedDeviceArrived);
            this.usb.OnSpecifiedDeviceRemoved += new System.EventHandler(this.usb_OnSpecifiedDeviceRemoved);
            this.usb.OnDeviceArrived += new System.EventHandler(this.usb_OnDeviceArrived);
            this.usb.OnDeviceRemoved += new System.EventHandler(this.usb_OnDeviceRemoved);
            this.usb.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(this.usb_OnDataRecieved);
            // 
            // tmr_USBConn
            // 
            this.tmr_USBConn.Enabled = true;
            this.tmr_USBConn.Interval = 500;
            this.tmr_USBConn.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // textBoxBoard
            // 
            this.textBoxBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxBoard, "textBoxBoard");
            this.textBoxBoard.ForeColor = System.Drawing.Color.White;
            this.textBoxBoard.Name = "textBoxBoard";
            // 
            // btn_Watch
            // 
            resources.ApplyResources(this.btn_Watch, "btn_Watch");
            this.btn_Watch.Name = "btn_Watch";
            this.btn_Watch.UseVisualStyleBackColor = true;
            this.btn_Watch.Click += new System.EventHandler(this.btn_Watch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lab_movement);
            this.groupBox3.Controls.Add(this.lab_status);
            this.groupBox3.Controls.Add(this.textBoxBoard);
            this.groupBox3.Controls.Add(this.Status1);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lab_movement
            // 
            this.lab_movement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lab_movement, "lab_movement");
            this.lab_movement.ForeColor = System.Drawing.Color.White;
            this.lab_movement.Name = "lab_movement";
            // 
            // lab_status
            // 
            this.lab_status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lab_status, "lab_status");
            this.lab_status.ForeColor = System.Drawing.Color.White;
            this.lab_status.Name = "lab_status";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_LCDOff);
            this.groupBox4.Controls.Add(this.btn_LCDOn);
            this.groupBox4.Controls.Add(this.btn_MANUAL_ARM);
            this.groupBox4.Controls.Add(this.btn_ResetARM);
            this.groupBox4.Controls.Add(this.btn_Head_LS);
            this.groupBox4.Controls.Add(this.btn_Head_RS);
            this.groupBox4.Controls.Add(this.btn_Head_RT);
            this.groupBox4.Controls.Add(this.btn_Head_UP);
            this.groupBox4.Controls.Add(this.btn_Head_LT);
            this.groupBox4.Controls.Add(this.btn_Head_Down);
            this.groupBox4.Controls.Add(this.btn_Neck_FT);
            this.groupBox4.Controls.Add(this.btn_Neck_BH);
            this.groupBox4.Controls.Add(this.btn_Neck_RT);
            this.groupBox4.Controls.Add(this.btn_Neck_UP);
            this.groupBox4.Controls.Add(this.btn_AUTO_ARM);
            this.groupBox4.Controls.Add(this.btn_Neck_LT);
            this.groupBox4.Controls.Add(this.btn_Neck_Down);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // btn_MANUAL_ARM
            // 
            this.btn_MANUAL_ARM.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_MANUAL_ARM, "btn_MANUAL_ARM");
            this.btn_MANUAL_ARM.Name = "btn_MANUAL_ARM";
            this.btn_MANUAL_ARM.UseVisualStyleBackColor = true;
            this.btn_MANUAL_ARM.Click += new System.EventHandler(this.btn_MANUAL_ARM_Click);
            // 
            // btn_ResetARM
            // 
            this.btn_ResetARM.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_ResetARM, "btn_ResetARM");
            this.btn_ResetARM.Name = "btn_ResetARM";
            this.btn_ResetARM.UseVisualStyleBackColor = true;
            this.btn_ResetARM.Click += new System.EventHandler(this.btn_ResetARM_Click);
            // 
            // btn_Head_LS
            // 
            this.btn_Head_LS.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_LS, "btn_Head_LS");
            this.btn_Head_LS.Name = "btn_Head_LS";
            this.btn_Head_LS.UseVisualStyleBackColor = true;
            this.btn_Head_LS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_LS_MouseDown);
            this.btn_Head_LS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_LS_MouseUp);
            // 
            // btn_Head_RS
            // 
            this.btn_Head_RS.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_RS, "btn_Head_RS");
            this.btn_Head_RS.Name = "btn_Head_RS";
            this.btn_Head_RS.UseVisualStyleBackColor = true;
            this.btn_Head_RS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_RS_MouseDown);
            this.btn_Head_RS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_RS_MouseUp);
            // 
            // btn_Head_RT
            // 
            this.btn_Head_RT.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_RT, "btn_Head_RT");
            this.btn_Head_RT.Name = "btn_Head_RT";
            this.btn_Head_RT.UseVisualStyleBackColor = true;
            this.btn_Head_RT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_RT_MouseDown);
            this.btn_Head_RT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_RT_MouseUp);
            // 
            // btn_Head_UP
            // 
            this.btn_Head_UP.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_UP, "btn_Head_UP");
            this.btn_Head_UP.Name = "btn_Head_UP";
            this.btn_Head_UP.UseVisualStyleBackColor = true;
            this.btn_Head_UP.Click += new System.EventHandler(this.btn_Head_UP_Click);
            this.btn_Head_UP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_UP_MouseDown);
            this.btn_Head_UP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_UP_MouseUp);
            // 
            // btn_Head_LT
            // 
            this.btn_Head_LT.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_LT, "btn_Head_LT");
            this.btn_Head_LT.Name = "btn_Head_LT";
            this.btn_Head_LT.UseVisualStyleBackColor = true;
            this.btn_Head_LT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_LT_MouseDown);
            this.btn_Head_LT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_LT_MouseUp);
            // 
            // btn_Head_Down
            // 
            this.btn_Head_Down.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Head_Down, "btn_Head_Down");
            this.btn_Head_Down.Name = "btn_Head_Down";
            this.btn_Head_Down.UseVisualStyleBackColor = true;
            this.btn_Head_Down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Head_Down_MouseDown);
            this.btn_Head_Down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Head_Down_MouseUp);
            // 
            // btn_Neck_FT
            // 
            this.btn_Neck_FT.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_FT, "btn_Neck_FT");
            this.btn_Neck_FT.Name = "btn_Neck_FT";
            this.btn_Neck_FT.UseVisualStyleBackColor = true;
            this.btn_Neck_FT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_FT_MouseDown);
            this.btn_Neck_FT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_FT_MouseUp);
            // 
            // btn_Neck_BH
            // 
            this.btn_Neck_BH.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_BH, "btn_Neck_BH");
            this.btn_Neck_BH.Name = "btn_Neck_BH";
            this.btn_Neck_BH.UseVisualStyleBackColor = true;
            this.btn_Neck_BH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_BH_MouseDown);
            this.btn_Neck_BH.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_BH_MouseUp);
            // 
            // btn_Neck_RT
            // 
            this.btn_Neck_RT.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_RT, "btn_Neck_RT");
            this.btn_Neck_RT.Name = "btn_Neck_RT";
            this.btn_Neck_RT.UseVisualStyleBackColor = true;
            this.btn_Neck_RT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_RT_MouseDown);
            this.btn_Neck_RT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_RT_MouseUp);
            // 
            // btn_Neck_UP
            // 
            this.btn_Neck_UP.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_UP, "btn_Neck_UP");
            this.btn_Neck_UP.Name = "btn_Neck_UP";
            this.btn_Neck_UP.UseVisualStyleBackColor = true;
            this.btn_Neck_UP.Click += new System.EventHandler(this.btn_Neck_UP_Click);
            this.btn_Neck_UP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_UP_MouseDown);
            this.btn_Neck_UP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_UP_MouseUp);
            // 
            // btn_AUTO_ARM
            // 
            this.btn_AUTO_ARM.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_AUTO_ARM, "btn_AUTO_ARM");
            this.btn_AUTO_ARM.Name = "btn_AUTO_ARM";
            this.btn_AUTO_ARM.UseVisualStyleBackColor = true;
            this.btn_AUTO_ARM.Click += new System.EventHandler(this.btn_AUTO_ARM_Click);
            // 
            // btn_Neck_LT
            // 
            this.btn_Neck_LT.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_LT, "btn_Neck_LT");
            this.btn_Neck_LT.Name = "btn_Neck_LT";
            this.btn_Neck_LT.UseVisualStyleBackColor = true;
            this.btn_Neck_LT.Click += new System.EventHandler(this.btn_Neck_LT_Click);
            this.btn_Neck_LT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_LT_MouseDown);
            this.btn_Neck_LT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_LT_MouseUp);
            // 
            // btn_Neck_Down
            // 
            this.btn_Neck_Down.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_Neck_Down, "btn_Neck_Down");
            this.btn_Neck_Down.Name = "btn_Neck_Down";
            this.btn_Neck_Down.UseVisualStyleBackColor = true;
            this.btn_Neck_Down.Click += new System.EventHandler(this.btn_Neck_Down_Click);
            this.btn_Neck_Down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_Down_MouseDown);
            this.btn_Neck_Down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Neck_Down_MouseUp);
            // 
            // grp_FaceSearch
            // 
            this.grp_FaceSearch.Controls.Add(this.pan_FaceSearch);
            this.grp_FaceSearch.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.grp_FaceSearch, "grp_FaceSearch");
            this.grp_FaceSearch.Name = "grp_FaceSearch";
            this.grp_FaceSearch.TabStop = false;
            // 
            // pan_FaceSearch
            // 
            this.pan_FaceSearch.Controls.Add(this.label1);
            this.pan_FaceSearch.Controls.Add(this.label36);
            this.pan_FaceSearch.Controls.Add(this.txt_FaceCentral);
            this.pan_FaceSearch.Controls.Add(this.txt_LR_Bias);
            this.pan_FaceSearch.Controls.Add(this.label39);
            this.pan_FaceSearch.Controls.Add(this.txt_UD_Bias);
            this.pan_FaceSearch.Controls.Add(this.txt_FaceLocked);
            this.pan_FaceSearch.Controls.Add(this.label35);
            this.pan_FaceSearch.Controls.Add(this.label34);
            this.pan_FaceSearch.Controls.Add(this.txt_FN_Bias);
            resources.ApplyResources(this.pan_FaceSearch, "pan_FaceSearch");
            this.pan_FaceSearch.Name = "pan_FaceSearch";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // txt_FaceCentral
            // 
            this.txt_FaceCentral.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_FaceCentral.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_FaceCentral, "txt_FaceCentral");
            this.txt_FaceCentral.Name = "txt_FaceCentral";
            // 
            // txt_LR_Bias
            // 
            this.txt_LR_Bias.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.txt_LR_Bias, "txt_LR_Bias");
            this.txt_LR_Bias.Name = "txt_LR_Bias";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // txt_UD_Bias
            // 
            this.txt_UD_Bias.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.txt_UD_Bias, "txt_UD_Bias");
            this.txt_UD_Bias.Name = "txt_UD_Bias";
            // 
            // txt_FaceLocked
            // 
            this.txt_FaceLocked.BackColor = System.Drawing.Color.Gainsboro;
            this.txt_FaceLocked.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txt_FaceLocked, "txt_FaceLocked");
            this.txt_FaceLocked.Name = "txt_FaceLocked";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // txt_FN_Bias
            // 
            this.txt_FN_Bias.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.txt_FN_Bias, "txt_FN_Bias");
            this.txt_FN_Bias.Name = "txt_FN_Bias";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // txt_Neck_LR
            // 
            resources.ApplyResources(this.txt_Neck_LR, "txt_Neck_LR");
            this.txt_Neck_LR.Name = "txt_Neck_LR";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // txt_Head_LR
            // 
            resources.ApplyResources(this.txt_Head_LR, "txt_Head_LR");
            this.txt_Head_LR.Name = "txt_Head_LR";
            this.txt_Head_LR.TextChanged += new System.EventHandler(this.txt_Head_LR_TextChanged);
            // 
            // btn_FarPilot
            // 
            resources.ApplyResources(this.btn_FarPilot, "btn_FarPilot");
            this.btn_FarPilot.Name = "btn_FarPilot";
            this.btn_FarPilot.UseVisualStyleBackColor = true;
            this.btn_FarPilot.Click += new System.EventHandler(this.btn_FarPilot_Click);
            // 
            // btn_NearPilot
            // 
            resources.ApplyResources(this.btn_NearPilot, "btn_NearPilot");
            this.btn_NearPilot.Name = "btn_NearPilot";
            this.btn_NearPilot.UseVisualStyleBackColor = true;
            this.btn_NearPilot.Click += new System.EventHandler(this.btn_NearPilot_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btn_Report);
            this.groupBox9.Controls.Add(this.btn_GetCmd);
            this.groupBox9.Controls.Add(this.rtb_Output);
            this.groupBox9.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // btn_Report
            // 
            resources.ApplyResources(this.btn_Report, "btn_Report");
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.UseVisualStyleBackColor = true;
            this.btn_Report.Click += new System.EventHandler(this.btn_Report_Click);
            // 
            // btn_GetCmd
            // 
            resources.ApplyResources(this.btn_GetCmd, "btn_GetCmd");
            this.btn_GetCmd.Name = "btn_GetCmd";
            this.btn_GetCmd.UseVisualStyleBackColor = true;
            this.btn_GetCmd.Click += new System.EventHandler(this.btn_GetCmd_Click);
            // 
            // rtb_Output
            // 
            this.rtb_Output.BackColor = System.Drawing.Color.Gainsboro;
            this.rtb_Output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.rtb_Output, "rtb_Output");
            this.rtb_Output.Name = "rtb_Output";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.label37);
            this.groupBox6.Controls.Add(this.tb_NeckSpeed);
            this.groupBox6.Controls.Add(this.tbHeadSpeed);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // tb_NeckSpeed
            // 
            this.tb_NeckSpeed.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.tb_NeckSpeed, "tb_NeckSpeed");
            this.tb_NeckSpeed.LargeChange = 2;
            this.tb_NeckSpeed.Minimum = 1;
            this.tb_NeckSpeed.Name = "tb_NeckSpeed";
            this.tb_NeckSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tb_NeckSpeed.Value = 2;
            // 
            // tbHeadSpeed
            // 
            this.tbHeadSpeed.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.tbHeadSpeed, "tbHeadSpeed");
            this.tbHeadSpeed.LargeChange = 2;
            this.tbHeadSpeed.Minimum = 1;
            this.tbHeadSpeed.Name = "tbHeadSpeed";
            this.tbHeadSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbHeadSpeed.Value = 5;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.groupBox7);
            this.groupBox10.Controls.Add(this.label41);
            this.groupBox10.Controls.Add(this.label40);
            this.groupBox10.Controls.Add(this.txt_Head_SLR_Speed);
            this.groupBox10.Controls.Add(this.txt_Neck_FN_Speed);
            this.groupBox10.Controls.Add(this.txt_Head_SLR);
            this.groupBox10.Controls.Add(this.txt_Neck_FN);
            this.groupBox10.Controls.Add(this.txt_Head_UD_Speed);
            this.groupBox10.Controls.Add(this.txt_Neck_UD_Speed);
            this.groupBox10.Controls.Add(this.txt_Head_UD);
            this.groupBox10.Controls.Add(this.txt_Neck_UD);
            this.groupBox10.Controls.Add(this.txt_Head_LR_Speed);
            this.groupBox10.Controls.Add(this.txt_Neck_LR_Speed);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.label33);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Controls.Add(this.txt_Head_LR);
            this.groupBox10.Controls.Add(this.txt_Neck_LR);
            this.groupBox10.Controls.Add(this.label32);
            this.groupBox10.Controls.Add(this.label31);
            this.groupBox10.Controls.Add(this.label30);
            this.groupBox10.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // txt_Head_SLR_Speed
            // 
            resources.ApplyResources(this.txt_Head_SLR_Speed, "txt_Head_SLR_Speed");
            this.txt_Head_SLR_Speed.Name = "txt_Head_SLR_Speed";
            // 
            // txt_Neck_FN_Speed
            // 
            resources.ApplyResources(this.txt_Neck_FN_Speed, "txt_Neck_FN_Speed");
            this.txt_Neck_FN_Speed.Name = "txt_Neck_FN_Speed";
            // 
            // txt_Head_SLR
            // 
            resources.ApplyResources(this.txt_Head_SLR, "txt_Head_SLR");
            this.txt_Head_SLR.Name = "txt_Head_SLR";
            // 
            // txt_Neck_FN
            // 
            resources.ApplyResources(this.txt_Neck_FN, "txt_Neck_FN");
            this.txt_Neck_FN.Name = "txt_Neck_FN";
            // 
            // txt_Head_UD_Speed
            // 
            resources.ApplyResources(this.txt_Head_UD_Speed, "txt_Head_UD_Speed");
            this.txt_Head_UD_Speed.Name = "txt_Head_UD_Speed";
            // 
            // txt_Neck_UD_Speed
            // 
            resources.ApplyResources(this.txt_Neck_UD_Speed, "txt_Neck_UD_Speed");
            this.txt_Neck_UD_Speed.Name = "txt_Neck_UD_Speed";
            // 
            // txt_Head_UD
            // 
            resources.ApplyResources(this.txt_Head_UD, "txt_Head_UD");
            this.txt_Head_UD.Name = "txt_Head_UD";
            // 
            // txt_Neck_UD
            // 
            resources.ApplyResources(this.txt_Neck_UD, "txt_Neck_UD");
            this.txt_Neck_UD.Name = "txt_Neck_UD";
            // 
            // txt_Head_LR_Speed
            // 
            resources.ApplyResources(this.txt_Head_LR_Speed, "txt_Head_LR_Speed");
            this.txt_Head_LR_Speed.Name = "txt_Head_LR_Speed";
            // 
            // txt_Neck_LR_Speed
            // 
            resources.ApplyResources(this.txt_Neck_LR_Speed, "txt_Neck_LR_Speed");
            this.txt_Neck_LR_Speed.Name = "txt_Neck_LR_Speed";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btn_SaveAllChannel);
            this.groupBox11.Controls.Add(this.btn_SaveDefaultPos);
            this.groupBox11.Controls.Add(this.btn_SaveChannel);
            this.groupBox11.Controls.Add(this.trb_Calc);
            this.groupBox11.Controls.Add(this.rad_HUD);
            this.groupBox11.Controls.Add(this.rad_NFN);
            this.groupBox11.Controls.Add(this.btn_SetDefaultPos);
            this.groupBox11.Controls.Add(this.rad_NUD);
            this.groupBox11.Controls.Add(this.rad_NLR);
            this.groupBox11.Controls.Add(this.rad_HTLR);
            this.groupBox11.Controls.Add(this.rad_HLR);
            this.groupBox11.Controls.Add(this.btn_SetLowLimit);
            this.groupBox11.Controls.Add(this.btn_SetHighLimit);
            this.groupBox11.Controls.Add(this.label44);
            resources.ApplyResources(this.groupBox11, "groupBox11");
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.TabStop = false;
            this.groupBox11.Enter += new System.EventHandler(this.groupBox11_Enter);
            // 
            // btn_SaveAllChannel
            // 
            resources.ApplyResources(this.btn_SaveAllChannel, "btn_SaveAllChannel");
            this.btn_SaveAllChannel.Name = "btn_SaveAllChannel";
            this.btn_SaveAllChannel.UseVisualStyleBackColor = true;
            this.btn_SaveAllChannel.Click += new System.EventHandler(this.btn_SaveAllChannel_Click);
            // 
            // btn_SaveDefaultPos
            // 
            resources.ApplyResources(this.btn_SaveDefaultPos, "btn_SaveDefaultPos");
            this.btn_SaveDefaultPos.Name = "btn_SaveDefaultPos";
            this.btn_SaveDefaultPos.UseVisualStyleBackColor = true;
            this.btn_SaveDefaultPos.Click += new System.EventHandler(this.btn_SaveDefaultPos_Click);
            // 
            // btn_SaveChannel
            // 
            resources.ApplyResources(this.btn_SaveChannel, "btn_SaveChannel");
            this.btn_SaveChannel.Name = "btn_SaveChannel";
            this.btn_SaveChannel.UseVisualStyleBackColor = true;
            this.btn_SaveChannel.Click += new System.EventHandler(this.btn_SaveChannel_Click);
            // 
            // trb_Calc
            // 
            resources.ApplyResources(this.trb_Calc, "trb_Calc");
            this.trb_Calc.Maximum = 180;
            this.trb_Calc.Name = "trb_Calc";
            this.trb_Calc.Scroll += new System.EventHandler(this.trb_Calc_Scroll);
            // 
            // rad_HUD
            // 
            resources.ApplyResources(this.rad_HUD, "rad_HUD");
            this.rad_HUD.Name = "rad_HUD";
            this.rad_HUD.UseVisualStyleBackColor = true;
            this.rad_HUD.CheckedChanged += new System.EventHandler(this.rad_Head_UD_CheckedChanged);
            // 
            // rad_NFN
            // 
            resources.ApplyResources(this.rad_NFN, "rad_NFN");
            this.rad_NFN.Name = "rad_NFN";
            this.rad_NFN.UseVisualStyleBackColor = true;
            // 
            // btn_SetDefaultPos
            // 
            resources.ApplyResources(this.btn_SetDefaultPos, "btn_SetDefaultPos");
            this.btn_SetDefaultPos.Name = "btn_SetDefaultPos";
            this.btn_SetDefaultPos.UseVisualStyleBackColor = true;
            this.btn_SetDefaultPos.Click += new System.EventHandler(this.btn_SetDefaultPos_Click);
            // 
            // rad_NUD
            // 
            resources.ApplyResources(this.rad_NUD, "rad_NUD");
            this.rad_NUD.Name = "rad_NUD";
            this.rad_NUD.UseVisualStyleBackColor = true;
            // 
            // rad_NLR
            // 
            resources.ApplyResources(this.rad_NLR, "rad_NLR");
            this.rad_NLR.Name = "rad_NLR";
            this.rad_NLR.UseVisualStyleBackColor = true;
            // 
            // rad_HTLR
            // 
            resources.ApplyResources(this.rad_HTLR, "rad_HTLR");
            this.rad_HTLR.Name = "rad_HTLR";
            this.rad_HTLR.UseVisualStyleBackColor = true;
            // 
            // rad_HLR
            // 
            resources.ApplyResources(this.rad_HLR, "rad_HLR");
            this.rad_HLR.Checked = true;
            this.rad_HLR.Name = "rad_HLR";
            this.rad_HLR.TabStop = true;
            this.rad_HLR.UseVisualStyleBackColor = true;
            // 
            // btn_SetLowLimit
            // 
            resources.ApplyResources(this.btn_SetLowLimit, "btn_SetLowLimit");
            this.btn_SetLowLimit.Name = "btn_SetLowLimit";
            this.btn_SetLowLimit.UseVisualStyleBackColor = true;
            this.btn_SetLowLimit.Click += new System.EventHandler(this.btn_SetLowLimit_Click);
            // 
            // btn_SetHighLimit
            // 
            resources.ApplyResources(this.btn_SetHighLimit, "btn_SetHighLimit");
            this.btn_SetHighLimit.Name = "btn_SetHighLimit";
            this.btn_SetHighLimit.UseVisualStyleBackColor = true;
            this.btn_SetHighLimit.Click += new System.EventHandler(this.btn_SetHighLimit_Click);
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            this.label44.Click += new System.EventHandler(this.label44_Click);
            // 
            // tmr_NearPilot
            // 
            this.tmr_NearPilot.Interval = 10;
            this.tmr_NearPilot.Tick += new System.EventHandler(this.tmr_NearPilot_Tick);
            // 
            // tmr_FarPilot
            // 
            this.tmr_FarPilot.Interval = 30;
            this.tmr_FarPilot.Tick += new System.EventHandler(this.tmr_FarPilot_Tick);
            // 
            // tmr_RefreshARMStatus
            // 
            this.tmr_RefreshARMStatus.Tick += new System.EventHandler(this.tmr_RefreshARMStatus_Tick);
            // 
            // tmr_RefreshFaceStatus
            // 
            this.tmr_RefreshFaceStatus.Enabled = true;
            this.tmr_RefreshFaceStatus.Tick += new System.EventHandler(this.tmr_RefreshFaceStatus_Tick);
            // 
            // btn_FaceFollow
            // 
            resources.ApplyResources(this.btn_FaceFollow, "btn_FaceFollow");
            this.btn_FaceFollow.Name = "btn_FaceFollow";
            this.btn_FaceFollow.UseVisualStyleBackColor = true;
            this.btn_FaceFollow.Click += new System.EventHandler(this.btn_FaceFollow_Click);
            // 
            // tmr_FaceFollow
            // 
            this.tmr_FaceFollow.Tick += new System.EventHandler(this.tmr_FaceFollow_Tick);
            // 
            // tmr_Watch
            // 
            this.tmr_Watch.Interval = 10;
            this.tmr_Watch.Tick += new System.EventHandler(this.tmr_Watch_Tick);
            // 
            // tmr_Run
            // 
            this.tmr_Run.Tick += new System.EventHandler(this.tmr_Run_Tick);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tmr_Cmd
            // 
            this.tmr_Cmd.Enabled = true;
            this.tmr_Cmd.Tick += new System.EventHandler(this.tmr_Cmd_Tick);
            // 
            // tmr_Report
            // 
            this.tmr_Report.Enabled = true;
            this.tmr_Report.Tick += new System.EventHandler(this.tmr_Report_Tick);
            // 
            // btn_LCDOn
            // 
            this.btn_LCDOn.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_LCDOn, "btn_LCDOn");
            this.btn_LCDOn.Name = "btn_LCDOn";
            this.btn_LCDOn.UseVisualStyleBackColor = true;
            this.btn_LCDOn.Click += new System.EventHandler(this.btn_LCDOn_Click);
            // 
            // btn_LCDOff
            // 
            this.btn_LCDOff.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btn_LCDOff, "btn_LCDOff");
            this.btn_LCDOff.Name = "btn_LCDOff";
            this.btn_LCDOff.UseVisualStyleBackColor = true;
            this.btn_LCDOff.Click += new System.EventHandler(this.btn_LCDOff_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_FaceFollow);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btn_FarPilot);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.btn_NearPilot);
            this.Controls.Add(this.btn_Watch);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.grp_FaceSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pan_Control);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pan_Control.ResumeLayout(false);
            this.pan_Control.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.grp_FaceSearch.ResumeLayout(false);
            this.pan_FaceSearch.ResumeLayout(false);
            this.pan_FaceSearch.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_NeckSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHeadSpeed)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Calc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_Control;
        private System.IO.Ports.SerialPort spSerialPort;
        private System.Windows.Forms.Label Status1;
        private UsbLibrary.UsbHidPort usb;
        private System.Windows.Forms.Timer tmr_USBConn;
        private System.Windows.Forms.Label textBoxBoard;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_Watch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_Head_LS;
        private System.Windows.Forms.Button btn_Head_RS;
        private System.Windows.Forms.Button btn_Head_RT;
        private System.Windows.Forms.Button btn_Head_UP;
        private System.Windows.Forms.Button btn_Head_LT;
        private System.Windows.Forms.Button btn_Head_Down;
        private System.Windows.Forms.Button btn_Neck_FT;
        private System.Windows.Forms.Button btn_Neck_BH;
        private System.Windows.Forms.Button btn_Neck_RT;
        private System.Windows.Forms.Button btn_Neck_UP;
        private System.Windows.Forms.Button btn_Neck_LT;
        private System.Windows.Forms.Button btn_Neck_Down;
        private System.Windows.Forms.Button btn_ResetARM;
        private System.Windows.Forms.Button btn_MANUAL_ARM;
        private System.Windows.Forms.Button btn_AUTO_ARM;
        private System.Windows.Forms.GroupBox grp_FaceSearch;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_Neck_LR;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_FN_Bias;
        private System.Windows.Forms.TextBox txt_Head_LR;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txt_UD_Bias;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txt_LR_Bias;
        private System.Windows.Forms.Button btn_FarPilot;
        private System.Windows.Forms.Button btn_NearPilot;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RichTextBox rtb_Output;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TrackBar tb_NeckSpeed;
        private System.Windows.Forms.TrackBar tbHeadSpeed;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txt_Head_SLR_Speed;
        private System.Windows.Forms.TextBox txt_Neck_FN_Speed;
        private System.Windows.Forms.TextBox txt_Head_SLR;
        private System.Windows.Forms.TextBox txt_Neck_FN;
        private System.Windows.Forms.TextBox txt_Head_UD_Speed;
        private System.Windows.Forms.TextBox txt_Neck_UD_Speed;
        private System.Windows.Forms.TextBox txt_Neck_UD;
        private System.Windows.Forms.TextBox txt_Head_LR_Speed;
        private System.Windows.Forms.TextBox txt_Neck_LR_Speed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txt_FaceLocked;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btn_SetDefaultPos;
        private System.Windows.Forms.RadioButton rad_HUD;
        private System.Windows.Forms.RadioButton rad_NFN;
        private System.Windows.Forms.RadioButton rad_NUD;
        private System.Windows.Forms.RadioButton rad_NLR;
        private System.Windows.Forms.RadioButton rad_HTLR;
        private System.Windows.Forms.RadioButton rad_HLR;
        private System.Windows.Forms.Button btn_SetLowLimit;
        private System.Windows.Forms.Button btn_SetHighLimit;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txt_Head_UD;
        private System.Windows.Forms.Timer tmr_NearPilot;
        private System.Windows.Forms.Timer tmr_FarPilot;
        private System.Windows.Forms.Timer tmr_RefreshARMStatus;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TrackBar trb_Calc;
        private System.Windows.Forms.Button btn_SaveChannel;
        private System.Windows.Forms.Button btn_SaveDefaultPos;
        private System.Windows.Forms.Button btn_SaveAllChannel;
        private System.Windows.Forms.Timer tmr_RefreshFaceStatus;
        private System.Windows.Forms.Button btn_FaceFollow;
        private System.Windows.Forms.Timer tmr_FaceFollow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_FaceCentral;
        private System.Windows.Forms.Timer tmr_Watch;
        private System.Windows.Forms.Timer tmr_Run;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pan_FaceSearch;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.Button btn_Report;
        private System.Windows.Forms.Button btn_GetCmd;
        private System.Windows.Forms.Label lab_movement;
        private System.Windows.Forms.Timer tmr_Cmd;
        private System.Windows.Forms.Timer tmr_Report;
        private System.Windows.Forms.Button btn_LCDOff;
        private System.Windows.Forms.Button btn_LCDOn;
    }
}

