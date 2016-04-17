using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
// IPEndPoint
using System.Net;

// Add a reference to NetworkingBasics.dll: classes used - BufferChunk
// Add a reference to LSTCommon.dll: classes used -  UnhandledExceptionHandler
using MSR.LST;              

// Add a reference to MSR.LST.Net.Rtp.dll
// Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using MSR.LST.Net.Rtp;      

// Code Flow (CF)
// 1. Hook Rtp events:
//   a.   RtpParticipant Added / Removed
//   b.   RtpStream Added / Removed
//   c.   Hook / Unhook FrameReceived event for that stream
// 2. Join RtpSession by providing an RtpParticipant and Multicast EndPoint
// 3. Retrieve RtpSender
// 4. Send data over network
// 5. Receive data from network
// 6. Unhook events, dispose RtpSession

namespace RtpChat
{
    public class frmChat : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code

        // Form variables
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnJoinLeave;
        private System.Windows.Forms.Button btnSend;
        private PictureBox pictureBox_Receive;
        private PictureBox pictureBox_sender;
        private Button button2;
        private TextBox text_IP_Multicast;
        private Label label1;
        private ListBox listBox1;
        private Button button1;
        private Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;

        // Required designer variable

        // Constructor
        public frmChat()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }


        // Required method for Designer support - do not modify
        // the contents of this method with the code editor.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.btnJoinLeave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox_sender = new System.Windows.Forms.PictureBox();
            this.pictureBox_Receive = new System.Windows.Forms.PictureBox();
            this.text_IP_Multicast = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_sender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Receive)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSend.CausesValidation = false;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(316, 74);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(81, 32);
            this.btnSend.TabIndex = 2;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "&Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(16, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Group";
            // 
            // btnJoinLeave
            // 
            this.btnJoinLeave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinLeave.Location = new System.Drawing.Point(213, 5);
            this.btnJoinLeave.Name = "btnJoinLeave";
            this.btnJoinLeave.Size = new System.Drawing.Size(75, 29);
            this.btnJoinLeave.TabIndex = 1;
            this.btnJoinLeave.Text = "Join";
            this.btnJoinLeave.Click += new System.EventHandler(this.btnJoinLeave_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.CausesValidation = false;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(316, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 32);
            this.button2.TabIndex = 9;
            this.button2.TabStop = false;
            this.button2.Text = "Stop";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox_sender
            // 
            this.pictureBox_sender.Image = global::RtpChat.Properties.Resources.DotNet;
            this.pictureBox_sender.Location = new System.Drawing.Point(303, 5);
            this.pictureBox_sender.Name = "pictureBox_sender";
            this.pictureBox_sender.Size = new System.Drawing.Size(100, 42);
            this.pictureBox_sender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_sender.TabIndex = 7;
            this.pictureBox_sender.TabStop = false;
            this.pictureBox_sender.Click += new System.EventHandler(this.pictureBox_sender_Click);
            // 
            // pictureBox_Receive
            // 
            this.pictureBox_Receive.Image = global::RtpChat.Properties.Resources.ip;
            this.pictureBox_Receive.Location = new System.Drawing.Point(12, 40);
            this.pictureBox_Receive.Name = "pictureBox_Receive";
            this.pictureBox_Receive.Size = new System.Drawing.Size(285, 170);
            this.pictureBox_Receive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Receive.TabIndex = 6;
            this.pictureBox_Receive.TabStop = false;
            this.pictureBox_Receive.Click += new System.EventHandler(this.pictureBox_Receive_Click);
            // 
            // text_IP_Multicast
            // 
            this.text_IP_Multicast.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_IP_Multicast.Location = new System.Drawing.Point(78, 5);
            this.text_IP_Multicast.MaxLength = 32;
            this.text_IP_Multicast.Name = "text_IP_Multicast";
            this.text_IP_Multicast.Size = new System.Drawing.Size(129, 26);
            this.text_IP_Multicast.TabIndex = 10;
            this.text_IP_Multicast.Text = "224.0.0.1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(455, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Join Log";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(428, 76);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(136, 134);
            this.listBox1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 15;
            this.button1.Text = "Camera On";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(490, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 44);
            this.button3.TabIndex = 16;
            this.button3.Text = "Camera Off";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmChat
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 217);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_IP_Multicast);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox_sender);
            this.Controls.Add(this.pictureBox_Receive);
            this.Controls.Add(this.btnJoinLeave);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnSend);
            this.MaximizeBox = false;
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTP Multicasting Video Conference -  (C) FADI Abdel-qader  www.fadidotnet.org";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmChat_Closing);
            this.Load += new System.EventHandler(this.frmChat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_sender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Receive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }


        #endregion

        #region Statics / App.Config overrides

        private static IPEndPoint ep;//= RtpSession.DefaultEndPoint;

        static frmChat()
        {
            string setting;

            // See if there was a multicast IP address set in the app.config
            if((setting = ConfigurationManager.AppSettings["EndPoint"]) != null)
            {
                string[] args = setting.Split(new char[]{':'}, 2);
                ep = new IPEndPoint(IPAddress.Parse(args[0]), int.Parse(args[1]));
            }
        }


        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles();
            // Make sure no exceptions escape unnoticed
            UnhandledExceptionHandler.Register();
            Application.Run(new frmChat());
        
        }


        #endregion Statics / App.Config overrides

        #region Members

        /// <summary>
        /// Manages the connection to a multicast address and all the objects related to Rtp
        /// </summary>
        private RtpSession rtpSession;

        /// <summary>
        /// Sends the data across the network
        /// </summary>
        private RtpSender rtpSender;

        #endregion Members

        #region WebCam API
        const short WM_CAP = 1024;
        const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        const int WM_CAP_EDIT_COPY = WM_CAP + 30;
        const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        const int WM_CAP_SET_SCALE = WM_CAP + 53;
        const int WS_CHILD = 1073741824;
        const int WS_VISIBLE = 268435456;
        const short SWP_NOMOVE = 2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 4;
        const short HWND_BOTTOM = 1;
        int iDevice = 0;
        int hHwnd;
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
			object lParam);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);
        private void OpenPreviewWindow()
        {
            int iHeight = 320;
            int iWidth = 200;
            // 
            //  Open Preview window in picturebox
            // 
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), (WS_VISIBLE | WS_CHILD), 0, 0, 640, 480, pictureBox_sender.Handle.ToInt32(), 0);
            // 
            //  Connect to device
            // 
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                // 
                // Set the preview scale
                // 
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);
                // 
                // Set the preview rate in milliseconds
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);
                // 
                // Start previewing the image from the camera
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);
                // 
                //  Resize window to fit in picturebox
                // 
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, iWidth, iHeight, (SWP_NOMOVE | SWP_NOZORDER));
            }
            else
            {
                // 
                //  Error connecting to device close window
                //  
                DestroyWindow(hHwnd);
            }
        }
        private void ClosePreviewWindow()
        {
            // 
            //  Disconnect from device
            // 
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0);
            // 
            //  close window
            // 
            DestroyWindow(hHwnd);
        }
        #endregion

        #region Private

        private void btnJoinLeave_Click(object sender, System.EventArgs e)
        {
            ep = new IPEndPoint(IPAddress.Parse(text_IP_Multicast.Text), 5000);
            if(btnJoinLeave.Text == "Join")
            {
                    HookRtpEvents(); // 1
                    JoinRtpSession(Dns.GetHostName()); // 2

                    // Change the UI
                    btnJoinLeave.Text = "Leave";
                    //txtSend.Enabled = true;
                    btnSend.Enabled = true;
                    button2.Enabled = true;
                    //txtSend.Focus();
                    text_IP_Multicast.Enabled = false;    
            }
            else
            {
                Cleanup(); // 6

                // Change the UI
                btnJoinLeave.Text = "Join";
                btnSend.Enabled = false;
                //txtReceive.Clear();
                text_IP_Multicast.Enabled = true;
                button2.Enabled = false;
            }
        }

        
        // CF1 Hook Rtp events
        private void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        
        // CF2 Create participant, join session
        // CF3 Retrieve RtpSender
        private void JoinRtpSession(string name)
        {
            rtpSession = new RtpSession(ep, new RtpParticipant(name, name), true, true);
            rtpSender = rtpSession.CreateRtpSenderFec(name, PayloadType.Chat, null, 0, 200);
        }
        MemoryStream ms;

        void send_img()
        {
            try
            {

                ms = new MemoryStream();// Store it in Binary Array as Stream


                IDataObject data;
                Image bmap;

                //  Copy image to clipboard
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);

                //  Get image from clipboard and convert it to a bitmap
                data = Clipboard.GetDataObject();

                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    bmap = ((Image)(data.GetData(typeof(System.Drawing.Bitmap))));
                    bmap.Save(ms, ImageFormat.Jpeg);
                }

                pictureBox_sender.Image.Save(ms, ImageFormat.Jpeg);
                rtpSender.Send(ms.ToArray ());
            }
            catch (Exception) { timer1.Enabled = false; }
        }
        // CF4 Send the data
        private void btnSend_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = true;
          
        }
        // CF5 Receive data from network
        private void RtpParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            ShowMessage(string.Format("{0} has joined", ea.RtpParticipant.Name));
        }

        private void RtpParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            ShowMessage(string.Format("{0} has left", ea.RtpParticipant.Name));
        }

        private void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            System.IO.MemoryStream ms = new MemoryStream(ea.Frame.Buffer);
            pictureBox_Receive.Image = Image.FromStream(ms);
        }


        // CF6 Unhook events, dispose RtpSession
        private void Cleanup()
        {
            UnhookRtpEvents();
            LeaveRtpSession();
        }

        private void UnhookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded -= new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        private void LeaveRtpSession()
        {
            if(rtpSession != null)
            {
                // Clean up all outstanding objects owned by the RtpSession
                rtpSession.Dispose();
                rtpSession = null;
                rtpSender = null;
            }
        }

        private void frmChat_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                
                Cleanup();
            }
            catch (Exception){}
        }       
      #endregion Private
        private void ShowMessage(string msg)
        {
            listBox1.Items.Add(msg);
        }

        private void frmChat_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iDevice = 0;
            OpenPreviewWindow();
	
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClosePreviewWindow();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            send_img();
        }

        private void pictureBox_sender_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Receive_Click(object sender, EventArgs e)
        {

        }
    }
}
