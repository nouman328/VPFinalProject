namespace RTP_VOIP_Library_Sample
{
    partial class RTP_VOIP_Library_Sample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Help_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TalkerName_textbox = new System.Windows.Forms.TextBox();
            this.IP_comboBox = new System.Windows.Forms.ComboBox();
            this.RTPP2P = new System.Windows.Forms.TabPage();
            this.button_RTPTalk = new System.Windows.Forms.Button();
            this.Direct_RTP_Port = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Leave = new System.Windows.Forms.Button();
            this.Talkers_listBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Join_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RTPRefServer = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.RefServ_SendingPort = new System.Windows.Forms.TextBox();
            this.button_Stop_Ref = new System.Windows.Forms.Button();
            this.RefServ_ListenPort = new System.Windows.Forms.TextBox();
            this.RefServ_TxtIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_Start_Ref = new System.Windows.Forms.Button();
            this.textBox_RefPort = new System.Windows.Forms.TextBox();
            this.textBox_RefUserName = new System.Windows.Forms.TextBox();
            this.textBox_RefIP = new System.Windows.Forms.ComboBox();
            this.Multicast_Group = new System.Windows.Forms.ComboBox();
            this.textBox_RefClientListenPort = new System.Windows.Forms.TextBox();
            this.Text_UDPPeerIP = new System.Windows.Forms.ComboBox();
            this.Text_UDPJoinToMCGroup = new System.Windows.Forms.ComboBox();
            this.textBox_UDPPort = new System.Windows.Forms.TextBox();
            this.textBox_UDPServerListeningPort = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.RTPRefClient = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button_CloseSession = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listBox_RefClientEvent = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_Conncet = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.UDP = new System.Windows.Forms.TabPage();
            this.button_UDPDropCall = new System.Windows.Forms.Button();
            this.button_JoinUDPMultiGroup = new System.Windows.Forms.Button();
            this.button_Hear = new System.Windows.Forms.Button();
            this.button_TalkUDP = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.button_StartUDPRefServer = new System.Windows.Forms.Button();
            this.button_StopUDPRefServer = new System.Windows.Forms.Button();
            this.textBox_UDPRefSendtoMCGroup = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.RTPP2P.SuspendLayout();
            this.RTPRefServer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.RTPRefClient.SuspendLayout();
            this.UDP.SuspendLayout();
            this.SuspendLayout();
            // 
            // Help_toolTip
            // 
            this.Help_toolTip.IsBalloon = true;
            this.Help_toolTip.ShowAlways = true;
            this.Help_toolTip.ToolTipTitle = "RTP Voice Library Help";
            // 
            // TalkerName_textbox
            // 
            this.TalkerName_textbox.Location = new System.Drawing.Point(60, 19);
            this.TalkerName_textbox.Name = "TalkerName_textbox";
            this.TalkerName_textbox.Size = new System.Drawing.Size(174, 20);
            this.TalkerName_textbox.TabIndex = 16;
            this.Help_toolTip.SetToolTip(this.TalkerName_textbox, "Your Nickname");
            // 
            // IP_comboBox
            // 
            this.IP_comboBox.FormattingEnabled = true;
            this.IP_comboBox.Items.AddRange(new object[] {
            "127.0.0.1",
            "224.2.0.1"});
            this.IP_comboBox.Location = new System.Drawing.Point(60, 66);
            this.IP_comboBox.Name = "IP_comboBox";
            this.IP_comboBox.Size = new System.Drawing.Size(174, 21);
            this.IP_comboBox.TabIndex = 14;
            this.IP_comboBox.Text = "127.0.0.1";
            this.Help_toolTip.SetToolTip(this.IP_comboBox, "Unicast or Multicast IP\r\nSend Directly to The Other Peer\r\n");
            // 
            // RTPP2P
            // 
            this.RTPP2P.Controls.Add(this.button_RTPTalk);
            this.RTPP2P.Controls.Add(this.Direct_RTP_Port);
            this.RTPP2P.Controls.Add(this.label7);
            this.RTPP2P.Controls.Add(this.TalkerName_textbox);
            this.RTPP2P.Controls.Add(this.label3);
            this.RTPP2P.Controls.Add(this.IP_comboBox);
            this.RTPP2P.Controls.Add(this.button_Leave);
            this.RTPP2P.Controls.Add(this.Talkers_listBox);
            this.RTPP2P.Controls.Add(this.label2);
            this.RTPP2P.Controls.Add(this.Join_button);
            this.RTPP2P.Controls.Add(this.label1);
            this.RTPP2P.Location = new System.Drawing.Point(4, 22);
            this.RTPP2P.Name = "RTPP2P";
            this.RTPP2P.Padding = new System.Windows.Forms.Padding(3);
            this.RTPP2P.Size = new System.Drawing.Size(437, 237);
            this.RTPP2P.TabIndex = 0;
            this.RTPP2P.Text = "Direct RTP Uni/Multicast";
            this.Help_toolTip.SetToolTip(this.RTPP2P, "Talk Peer-to-Peer Or to Multicast Group Directly");
            this.RTPP2P.UseVisualStyleBackColor = true;
            // 
            // button_RTPTalk
            // 
            this.button_RTPTalk.Enabled = false;
            this.button_RTPTalk.Location = new System.Drawing.Point(321, 17);
            this.button_RTPTalk.Name = "button_RTPTalk";
            this.button_RTPTalk.Size = new System.Drawing.Size(75, 23);
            this.button_RTPTalk.TabIndex = 19;
            this.button_RTPTalk.Text = "Talk";
            this.button_RTPTalk.UseVisualStyleBackColor = true;
            this.button_RTPTalk.Click += new System.EventHandler(this.button_RTPTalk_Click);
            // 
            // Direct_RTP_Port
            // 
            this.Direct_RTP_Port.Location = new System.Drawing.Point(60, 43);
            this.Direct_RTP_Port.Name = "Direct_RTP_Port";
            this.Direct_RTP_Port.Size = new System.Drawing.Size(174, 20);
            this.Direct_RTP_Port.TabIndex = 18;
            this.Direct_RTP_Port.Text = "5500";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "RTP Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Your Name";
            // 
            // button_Leave
            // 
            this.button_Leave.Enabled = false;
            this.button_Leave.Location = new System.Drawing.Point(240, 46);
            this.button_Leave.Name = "button_Leave";
            this.button_Leave.Size = new System.Drawing.Size(156, 23);
            this.button_Leave.TabIndex = 13;
            this.button_Leave.Text = "Leave";
            this.button_Leave.UseVisualStyleBackColor = true;
            this.button_Leave.Click += new System.EventHandler(this.button_Leave_Click_1);
            // 
            // Talkers_listBox
            // 
            this.Talkers_listBox.FormattingEnabled = true;
            this.Talkers_listBox.Location = new System.Drawing.Point(8, 142);
            this.Talkers_listBox.Name = "Talkers_listBox";
            this.Talkers_listBox.Size = new System.Drawing.Size(421, 82);
            this.Talkers_listBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Log Events";
            // 
            // Join_button
            // 
            this.Join_button.Location = new System.Drawing.Point(240, 17);
            this.Join_button.Name = "Join_button";
            this.Join_button.Size = new System.Drawing.Size(75, 23);
            this.Join_button.TabIndex = 10;
            this.Join_button.Text = "Join";
            this.Join_button.UseVisualStyleBackColor = true;
            this.Join_button.Click += new System.EventHandler(this.Join_button_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Peer IP";
            // 
            // RTPRefServer
            // 
            this.RTPRefServer.Controls.Add(this.label6);
            this.RTPRefServer.Controls.Add(this.RefServ_SendingPort);
            this.RTPRefServer.Controls.Add(this.button_Stop_Ref);
            this.RTPRefServer.Controls.Add(this.RefServ_ListenPort);
            this.RTPRefServer.Controls.Add(this.RefServ_TxtIP);
            this.RTPRefServer.Controls.Add(this.label5);
            this.RTPRefServer.Controls.Add(this.label4);
            this.RTPRefServer.Controls.Add(this.button_Start_Ref);
            this.RTPRefServer.Location = new System.Drawing.Point(4, 22);
            this.RTPRefServer.Name = "RTPRefServer";
            this.RTPRefServer.Size = new System.Drawing.Size(437, 237);
            this.RTPRefServer.TabIndex = 2;
            this.RTPRefServer.Text = "RTP Reflector Service";
            this.Help_toolTip.SetToolTip(this.RTPRefServer, "Reflector Service use to reflect any incomming unicast message to multicast group" +
                    " using RTP Protocol");
            this.RTPRefServer.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Send To Port";
            // 
            // RefServ_SendingPort
            // 
            this.RefServ_SendingPort.Location = new System.Drawing.Point(134, 46);
            this.RefServ_SendingPort.Name = "RefServ_SendingPort";
            this.RefServ_SendingPort.Size = new System.Drawing.Size(137, 20);
            this.RefServ_SendingPort.TabIndex = 6;
            this.RefServ_SendingPort.Text = "5600";
            this.Help_toolTip.SetToolTip(this.RefServ_SendingPort, "Client  Listening Port");
            // 
            // button_Stop_Ref
            // 
            this.button_Stop_Ref.Enabled = false;
            this.button_Stop_Ref.Location = new System.Drawing.Point(277, 85);
            this.button_Stop_Ref.Name = "button_Stop_Ref";
            this.button_Stop_Ref.Size = new System.Drawing.Size(75, 23);
            this.button_Stop_Ref.TabIndex = 5;
            this.button_Stop_Ref.Text = "Stop";
            this.button_Stop_Ref.UseVisualStyleBackColor = true;
            this.button_Stop_Ref.Click += new System.EventHandler(this.button_Stop_Ref_Click);
            // 
            // RefServ_ListenPort
            // 
            this.RefServ_ListenPort.Location = new System.Drawing.Point(134, 20);
            this.RefServ_ListenPort.Name = "RefServ_ListenPort";
            this.RefServ_ListenPort.Size = new System.Drawing.Size(137, 20);
            this.RefServ_ListenPort.TabIndex = 4;
            this.RefServ_ListenPort.Text = "5500";
            this.Help_toolTip.SetToolTip(this.RefServ_ListenPort, "Server Listening Port");
            // 
            // RefServ_TxtIP
            // 
            this.RefServ_TxtIP.Location = new System.Drawing.Point(134, 88);
            this.RefServ_TxtIP.Name = "RefServ_TxtIP";
            this.RefServ_TxtIP.Size = new System.Drawing.Size(137, 20);
            this.RefServ_TxtIP.TabIndex = 3;
            this.RefServ_TxtIP.Text = "224.2.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Listen On Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Reflect to MulticastIP";
            // 
            // button_Start_Ref
            // 
            this.button_Start_Ref.Location = new System.Drawing.Point(277, 20);
            this.button_Start_Ref.Name = "button_Start_Ref";
            this.button_Start_Ref.Size = new System.Drawing.Size(75, 23);
            this.button_Start_Ref.TabIndex = 0;
            this.button_Start_Ref.Text = "Start";
            this.button_Start_Ref.UseVisualStyleBackColor = true;
            this.button_Start_Ref.Click += new System.EventHandler(this.button_Start_Ref_Click);
            // 
            // textBox_RefPort
            // 
            this.textBox_RefPort.Location = new System.Drawing.Point(92, 39);
            this.textBox_RefPort.Name = "textBox_RefPort";
            this.textBox_RefPort.Size = new System.Drawing.Size(147, 20);
            this.textBox_RefPort.TabIndex = 28;
            this.textBox_RefPort.Text = "5500";
            this.Help_toolTip.SetToolTip(this.textBox_RefPort, "Server Listening Port\r\n");
            // 
            // textBox_RefUserName
            // 
            this.textBox_RefUserName.Location = new System.Drawing.Point(92, 15);
            this.textBox_RefUserName.Name = "textBox_RefUserName";
            this.textBox_RefUserName.Size = new System.Drawing.Size(147, 20);
            this.textBox_RefUserName.TabIndex = 26;
            this.Help_toolTip.SetToolTip(this.textBox_RefUserName, "Your Nickname");
            // 
            // textBox_RefIP
            // 
            this.textBox_RefIP.FormattingEnabled = true;
            this.textBox_RefIP.Items.AddRange(new object[] {
            "127.0.0.1"});
            this.textBox_RefIP.Location = new System.Drawing.Point(92, 89);
            this.textBox_RefIP.Name = "textBox_RefIP";
            this.textBox_RefIP.Size = new System.Drawing.Size(147, 21);
            this.textBox_RefIP.TabIndex = 24;
            this.textBox_RefIP.Text = "127.0.0.1";
            this.Help_toolTip.SetToolTip(this.textBox_RefIP, "Reflector IP Address (Unicast IP)\r\n\r\nPlease Note That you cannot Start The Reflec" +
                    "tor Service on the localhost\r\nso you have to change this IP to your Reflector Se" +
                    "rver IP");
            // 
            // Multicast_Group
            // 
            this.Multicast_Group.FormattingEnabled = true;
            this.Multicast_Group.Items.AddRange(new object[] {
            "224.2.0.1"});
            this.Multicast_Group.Location = new System.Drawing.Point(92, 119);
            this.Multicast_Group.Name = "Multicast_Group";
            this.Multicast_Group.Size = new System.Drawing.Size(147, 21);
            this.Multicast_Group.TabIndex = 30;
            this.Multicast_Group.Text = "224.2.0.1";
            this.Help_toolTip.SetToolTip(this.Multicast_Group, "Receiving the Multicast Voice From An RTP Reflector Server");
            // 
            // textBox_RefClientListenPort
            // 
            this.textBox_RefClientListenPort.Location = new System.Drawing.Point(92, 61);
            this.textBox_RefClientListenPort.Name = "textBox_RefClientListenPort";
            this.textBox_RefClientListenPort.Size = new System.Drawing.Size(147, 20);
            this.textBox_RefClientListenPort.TabIndex = 34;
            this.textBox_RefClientListenPort.Text = "5600";
            this.Help_toolTip.SetToolTip(this.textBox_RefClientListenPort, "Client  Listening Port");
            // 
            // Text_UDPPeerIP
            // 
            this.Text_UDPPeerIP.FormattingEnabled = true;
            this.Text_UDPPeerIP.Items.AddRange(new object[] {
            "127.0.0.1",
            "234.5.7.9"});
            this.Text_UDPPeerIP.Location = new System.Drawing.Point(139, 42);
            this.Text_UDPPeerIP.Name = "Text_UDPPeerIP";
            this.Text_UDPPeerIP.Size = new System.Drawing.Size(118, 21);
            this.Text_UDPPeerIP.TabIndex = 22;
            this.Text_UDPPeerIP.Text = "127.0.0.1";
            this.Help_toolTip.SetToolTip(this.Text_UDPPeerIP, "Unicast or Multicast IP\r\n");
            // 
            // Text_UDPJoinToMCGroup
            // 
            this.Text_UDPJoinToMCGroup.FormattingEnabled = true;
            this.Text_UDPJoinToMCGroup.Items.AddRange(new object[] {
            "224.2.0.1"});
            this.Text_UDPJoinToMCGroup.Location = new System.Drawing.Point(109, 71);
            this.Text_UDPJoinToMCGroup.Name = "Text_UDPJoinToMCGroup";
            this.Text_UDPJoinToMCGroup.Size = new System.Drawing.Size(148, 21);
            this.Text_UDPJoinToMCGroup.TabIndex = 35;
            this.Text_UDPJoinToMCGroup.Text = "224.2.0.1";
            this.Help_toolTip.SetToolTip(this.Text_UDPJoinToMCGroup, "Multicast IP\r\n");
            // 
            // textBox_UDPPort
            // 
            this.textBox_UDPPort.Location = new System.Drawing.Point(41, 42);
            this.textBox_UDPPort.Name = "textBox_UDPPort";
            this.textBox_UDPPort.Size = new System.Drawing.Size(69, 20);
            this.textBox_UDPPort.TabIndex = 24;
            this.textBox_UDPPort.Text = "6000";
            this.Help_toolTip.SetToolTip(this.textBox_UDPPort, "Client Listening Port");
            // 
            // textBox_UDPServerListeningPort
            // 
            this.textBox_UDPServerListeningPort.Location = new System.Drawing.Point(144, 165);
            this.textBox_UDPServerListeningPort.Name = "textBox_UDPServerListeningPort";
            this.textBox_UDPServerListeningPort.Size = new System.Drawing.Size(137, 20);
            this.textBox_UDPServerListeningPort.TabIndex = 30;
            this.textBox_UDPServerListeningPort.Text = "6000";
            this.Help_toolTip.SetToolTip(this.textBox_UDPServerListeningPort, "Server Listening Port");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.RTPP2P);
            this.tabControl1.Controls.Add(this.RTPRefClient);
            this.tabControl1.Controls.Add(this.RTPRefServer);
            this.tabControl1.Controls.Add(this.UDP);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(445, 263);
            this.tabControl1.TabIndex = 0;
            // 
            // RTPRefClient
            // 
            this.RTPRefClient.Controls.Add(this.textBox_RefClientListenPort);
            this.RTPRefClient.Controls.Add(this.label13);
            this.RTPRefClient.Controls.Add(this.label12);
            this.RTPRefClient.Controls.Add(this.button_CloseSession);
            this.RTPRefClient.Controls.Add(this.Multicast_Group);
            this.RTPRefClient.Controls.Add(this.button2);
            this.RTPRefClient.Controls.Add(this.textBox_RefPort);
            this.RTPRefClient.Controls.Add(this.label8);
            this.RTPRefClient.Controls.Add(this.textBox_RefUserName);
            this.RTPRefClient.Controls.Add(this.label9);
            this.RTPRefClient.Controls.Add(this.textBox_RefIP);
            this.RTPRefClient.Controls.Add(this.listBox_RefClientEvent);
            this.RTPRefClient.Controls.Add(this.label10);
            this.RTPRefClient.Controls.Add(this.button_Conncet);
            this.RTPRefClient.Controls.Add(this.label11);
            this.RTPRefClient.Location = new System.Drawing.Point(4, 22);
            this.RTPRefClient.Name = "RTPRefClient";
            this.RTPRefClient.Padding = new System.Windows.Forms.Padding(3);
            this.RTPRefClient.Size = new System.Drawing.Size(437, 237);
            this.RTPRefClient.TabIndex = 1;
            this.RTPRefClient.Text = "Conncet to RTP Reflector";
            this.RTPRefClient.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Listening Port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Multicast Group";
            // 
            // button_CloseSession
            // 
            this.button_CloseSession.Enabled = false;
            this.button_CloseSession.Location = new System.Drawing.Point(245, 117);
            this.button_CloseSession.Name = "button_CloseSession";
            this.button_CloseSession.Size = new System.Drawing.Size(113, 23);
            this.button_CloseSession.TabIndex = 31;
            this.button_CloseSession.Text = "Leave";
            this.button_CloseSession.UseVisualStyleBackColor = true;
            this.button_CloseSession.Click += new System.EventHandler(this.button_CloseSession_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Join And Receive";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Server Port";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Your Name";
            // 
            // listBox_RefClientEvent
            // 
            this.listBox_RefClientEvent.FormattingEnabled = true;
            this.listBox_RefClientEvent.Location = new System.Drawing.Point(6, 169);
            this.listBox_RefClientEvent.Name = "listBox_RefClientEvent";
            this.listBox_RefClientEvent.Size = new System.Drawing.Size(367, 56);
            this.listBox_RefClientEvent.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Log Events";
            // 
            // button_Conncet
            // 
            this.button_Conncet.Location = new System.Drawing.Point(245, 36);
            this.button_Conncet.Name = "button_Conncet";
            this.button_Conncet.Size = new System.Drawing.Size(113, 23);
            this.button_Conncet.TabIndex = 20;
            this.button_Conncet.Text = "Talking";
            this.button_Conncet.UseVisualStyleBackColor = true;
            this.button_Conncet.Click += new System.EventHandler(this.button_Conncet_Click_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Reflector Server";
            // 
            // UDP
            // 
            this.UDP.Controls.Add(this.button_UDPDropCall);
            this.UDP.Controls.Add(this.button_JoinUDPMultiGroup);
            this.UDP.Controls.Add(this.button_Hear);
            this.UDP.Controls.Add(this.button_TalkUDP);
            this.UDP.Controls.Add(this.Text_UDPJoinToMCGroup);
            this.UDP.Controls.Add(this.label24);
            this.UDP.Controls.Add(this.label23);
            this.UDP.Controls.Add(this.label22);
            this.UDP.Controls.Add(this.label21);
            this.UDP.Controls.Add(this.textBox_UDPServerListeningPort);
            this.UDP.Controls.Add(this.label20);
            this.UDP.Controls.Add(this.button_StartUDPRefServer);
            this.UDP.Controls.Add(this.button_StopUDPRefServer);
            this.UDP.Controls.Add(this.textBox_UDPRefSendtoMCGroup);
            this.UDP.Controls.Add(this.label19);
            this.UDP.Controls.Add(this.textBox_UDPPort);
            this.UDP.Controls.Add(this.label14);
            this.UDP.Controls.Add(this.Text_UDPPeerIP);
            this.UDP.Controls.Add(this.label15);
            this.UDP.Location = new System.Drawing.Point(4, 22);
            this.UDP.Name = "UDP";
            this.UDP.Padding = new System.Windows.Forms.Padding(3);
            this.UDP.Size = new System.Drawing.Size(437, 237);
            this.UDP.TabIndex = 3;
            this.UDP.Text = "UDP";
            this.UDP.UseVisualStyleBackColor = true;
            // 
            // button_UDPDropCall
            // 
            this.button_UDPDropCall.Enabled = false;
            this.button_UDPDropCall.Location = new System.Drawing.Point(304, 103);
            this.button_UDPDropCall.Name = "button_UDPDropCall";
            this.button_UDPDropCall.Size = new System.Drawing.Size(58, 23);
            this.button_UDPDropCall.TabIndex = 40;
            this.button_UDPDropCall.Text = "End Call";
            this.button_UDPDropCall.UseVisualStyleBackColor = true;
            this.button_UDPDropCall.Click += new System.EventHandler(this.button_UDPDropCall_Click);
            // 
            // button_JoinUDPMultiGroup
            // 
            this.button_JoinUDPMultiGroup.Location = new System.Drawing.Point(263, 71);
            this.button_JoinUDPMultiGroup.Name = "button_JoinUDPMultiGroup";
            this.button_JoinUDPMultiGroup.Size = new System.Drawing.Size(149, 23);
            this.button_JoinUDPMultiGroup.TabIndex = 39;
            this.button_JoinUDPMultiGroup.Text = "Hear From Multicast Group";
            this.button_JoinUDPMultiGroup.UseVisualStyleBackColor = true;
            this.button_JoinUDPMultiGroup.Click += new System.EventHandler(this.button_JoinUDPMultiGroup_Click);
            // 
            // button_Hear
            // 
            this.button_Hear.Location = new System.Drawing.Point(319, 42);
            this.button_Hear.Name = "button_Hear";
            this.button_Hear.Size = new System.Drawing.Size(93, 23);
            this.button_Hear.TabIndex = 38;
            this.button_Hear.Text = "Hear Unicasting";
            this.button_Hear.UseVisualStyleBackColor = true;
            this.button_Hear.Click += new System.EventHandler(this.button_Hear_Click);
            // 
            // button_TalkUDP
            // 
            this.button_TalkUDP.Location = new System.Drawing.Point(263, 42);
            this.button_TalkUDP.Name = "button_TalkUDP";
            this.button_TalkUDP.Size = new System.Drawing.Size(50, 23);
            this.button_TalkUDP.TabIndex = 37;
            this.button_TalkUDP.Text = "Talk";
            this.button_TalkUDP.UseVisualStyleBackColor = true;
            this.button_TalkUDP.Click += new System.EventHandler(this.button_TalkUDP_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 76);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(81, 13);
            this.label24.TabIndex = 34;
            this.label24.Text = "Multicast Group";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(78, 12);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(272, 13);
            this.label23.TabIndex = 33;
            this.label23.Text = "PeerToPeer Or PeerToGroup Or PeerToReflectorServer";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(150, 142);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(109, 13);
            this.label22.TabIndex = 32;
            this.label22.Text = "UDP Reflector Server";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(29, 129);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(343, 13);
            this.label21.TabIndex = 31;
            this.label21.Text = "________________________________________________________";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(29, 167);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(107, 13);
            this.label20.TabIndex = 29;
            this.label20.Text = "Server Listening Port";
            // 
            // button_StartUDPRefServer
            // 
            this.button_StartUDPRefServer.Location = new System.Drawing.Point(287, 165);
            this.button_StartUDPRefServer.Name = "button_StartUDPRefServer";
            this.button_StartUDPRefServer.Size = new System.Drawing.Size(75, 23);
            this.button_StartUDPRefServer.TabIndex = 28;
            this.button_StartUDPRefServer.Text = "Start";
            this.button_StartUDPRefServer.UseVisualStyleBackColor = true;
            this.button_StartUDPRefServer.Click += new System.EventHandler(this.button_StartUDPRefServer_Click);
            // 
            // button_StopUDPRefServer
            // 
            this.button_StopUDPRefServer.Enabled = false;
            this.button_StopUDPRefServer.Location = new System.Drawing.Point(287, 198);
            this.button_StopUDPRefServer.Name = "button_StopUDPRefServer";
            this.button_StopUDPRefServer.Size = new System.Drawing.Size(75, 23);
            this.button_StopUDPRefServer.TabIndex = 27;
            this.button_StopUDPRefServer.Text = "Stop";
            this.button_StopUDPRefServer.UseVisualStyleBackColor = true;
            this.button_StopUDPRefServer.Click += new System.EventHandler(this.button_StopUDPRefServer_Click);
            // 
            // textBox_UDPRefSendtoMCGroup
            // 
            this.textBox_UDPRefSendtoMCGroup.Location = new System.Drawing.Point(144, 201);
            this.textBox_UDPRefSendtoMCGroup.Name = "textBox_UDPRefSendtoMCGroup";
            this.textBox_UDPRefSendtoMCGroup.Size = new System.Drawing.Size(137, 20);
            this.textBox_UDPRefSendtoMCGroup.TabIndex = 26;
            this.textBox_UDPRefSendtoMCGroup.Text = "224.2.0.1";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(29, 204);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(109, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "Reflect to MulticastIP";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Port";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(116, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "IP";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(277, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(134, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(137, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "5500";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Listen On Port";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Send To Port";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(134, 46);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(137, 20);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "5000";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(134, 88);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(137, 20);
            this.textBox4.TabIndex = 3;
            this.textBox4.Text = "234.5.7.9";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 91);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(109, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Reflect to MulticastIP";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(277, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // RTP_VOIP_Library_Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 263);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RTP_VOIP_Library_Sample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTP VOIP Library - (C) SocketCoder Labs, SocketCoder.Com";
            this.Help_toolTip.SetToolTip(this, "Reflector Service use to reflect any incoming unicast message to multicast group " +
                    "using RTP Protocol");
            this.Load += new System.EventHandler(this.RTP_VOIP_Library_Sample_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RTPVoiceConferencingSample_FormClosing);
            this.RTPP2P.ResumeLayout(false);
            this.RTPP2P.PerformLayout();
            this.RTPRefServer.ResumeLayout(false);
            this.RTPRefServer.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.RTPRefClient.ResumeLayout(false);
            this.RTPRefClient.PerformLayout();
            this.UDP.ResumeLayout(false);
            this.UDP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip Help_toolTip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RTPP2P;
        private System.Windows.Forms.TextBox TalkerName_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox IP_comboBox;
        private System.Windows.Forms.Button button_Leave;
        private System.Windows.Forms.ListBox Talkers_listBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Join_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage RTPRefClient;
        private System.Windows.Forms.TabPage RTPRefServer;
        private System.Windows.Forms.TextBox RefServ_ListenPort;
        private System.Windows.Forms.TextBox RefServ_TxtIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_Start_Ref;
        private System.Windows.Forms.Button button_Stop_Ref;
        private System.Windows.Forms.TextBox Direct_RTP_Port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_RefPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_RefUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox textBox_RefIP;
        private System.Windows.Forms.ListBox listBox_RefClientEvent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_Conncet;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox Multicast_Group;
        private System.Windows.Forms.Button button_CloseSession;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RefServ_SendingPort;
        private System.Windows.Forms.TextBox textBox_RefClientListenPort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage UDP;
        private System.Windows.Forms.TextBox textBox_UDPPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox Text_UDPPeerIP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_UDPServerListeningPort;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button_StartUDPRefServer;
        private System.Windows.Forms.Button button_StopUDPRefServer;
        private System.Windows.Forms.TextBox textBox_UDPRefSendtoMCGroup;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox Text_UDPJoinToMCGroup;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button_Hear;
        private System.Windows.Forms.Button button_TalkUDP;
        private System.Windows.Forms.Button button_JoinUDPMultiGroup;
        private System.Windows.Forms.Button button_UDPDropCall;
        private System.Windows.Forms.Button button_RTPTalk;
    }
}

