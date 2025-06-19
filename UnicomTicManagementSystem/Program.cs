using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Controller;
using UnicomTicManagementSystem.Repositories;
using UnicomTicManagementSystem.View;

namespace UnicomTicManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            TableCreateQuery.CreateTables();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FirstForm());

           
        }
    }
}
