using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Lifetime;

namespace GeneticMarket.Console
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LifetimeServices.LeaseTime = TimeSpan.FromMinutes(100);
            LifetimeServices.RenewOnCallTime = TimeSpan.FromMinutes(150);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
