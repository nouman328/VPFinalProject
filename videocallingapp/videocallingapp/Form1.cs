using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace videocallingapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axVideoChatSender1.VideoDevice = 0;
            axVideoChatSender1.AudioDevice = 0;
            axVideoChatSender1.VideoFormat = 0;
            axVideoChatSender1.FrameRate = 15;
            axVideoChatSender1.VideoBitrate = 128000;
            axVideoChatSender1.AudioComplexity = 0;
            axVideoChatSender1.AudioQuality = 8;
            axVideoChatSender1.SendAudioStream = true ;
            axVideoChatSender1.SendVideoStream = true;

            axVideoChatSender1.Connect("127.0.0.1 " , 1234 );
        }
    }
}
