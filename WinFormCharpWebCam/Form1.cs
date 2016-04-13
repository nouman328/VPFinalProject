using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using WebCam_Capture;



namespace WinFormCharpWebCam
{
    //Design by Pongsakorn Poosankam
    public partial class mainWinForm : Form
    {
        public mainWinForm()
        {
            InitializeComponent();
        }
        WebCam webcam;
        private void mainWinForm_Load(object sender, EventArgs e)
        {
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo);
        }

        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        

        
    }
}
