using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
        }


        Timer tmr;

        private void splashscreen_Shown(object sender, EventArgs e)

        {

            tmr = new Timer();

            //set time interval 3 sec

            tmr.Interval = 3000;

            //starts the timer

            tmr.Start();

            tmr.Tick += tmr_Tick;

        }

        void tmr_Tick(object sender, EventArgs e)

        {

            //after 3 sec stop the timer

            tmr.Stop();

            //display mainform

            Form1 one = new Form1();

            one.Show();

            //hide this form

            this.Hide();

        }
    }
}
