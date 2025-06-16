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
        static async Task Main()
        {
           TableCreateQuery.CreateTables();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginController = new LoginController();
            bool hasUser = await loginController.IsAnyUSerExitsAsync();
            if (hasUser)
            {
            Application.Run(new UserCreateForm());

            }
            else
            {
                Application.Run(new UserLoginCreate());
            }
        }
    }
}
