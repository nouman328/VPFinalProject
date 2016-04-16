using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RTP_VOIP_Library_Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RTP_VOIP_Library_Sample());
        }
    }
}
