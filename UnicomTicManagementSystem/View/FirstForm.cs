using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Controller;

namespace UnicomTicManagementSystem.View
{
    public partial class FirstForm : Form
    {
        public FirstForm()
        {
            InitializeComponent();
        }


        private void FirstForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
        }

       
        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var loginController = new LoginController();
            bool hasUser = loginController.IsAnyUSerExitsAsync().GetAwaiter().GetResult();
            if (hasUser)
            {
                this.Hide();
                var usercreateform = new UserCreateForm();
                usercreateform.ShowDialog();
                //Application.Run(new UserCreateForm());

            }
            else
            {
                this.Hide();
                var urercreateform = new UserLoginCreate();
                urercreateform.ShowDialog();
            }
        }
    }
}
