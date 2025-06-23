using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem;
using UnicomTicManagementSystem.View;

namespace UnicomTicManagementSystem.View
{
    public partial class StaffViewForm : Form
    {
        public StaffViewForm()
        {
            InitializeComponent();
            LoadForm(new MainDashboad());
        }
        public void LoadForm(Form form)
        {

            foreach (Control ctrl in Staffpanel.Controls)
            {
                ctrl.Dispose();
            }
            Staffpanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            Staffpanel.Controls.Add(form);
            form.Show();

        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            LoadForm(new StudentManege());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm("Staff");
            loginForm.TopLevel = false;
            loginForm.FormBorderStyle = FormBorderStyle.None;
            loginForm.Dock = DockStyle.Fill;

            this.Controls.Clear();
            this.Controls.Add(loginForm);
            loginForm.Show();
        }

        private void btneditMark_Click(object sender, EventArgs e)
        {
            LoadForm(new MarkForm());
        }

        private void btnAddLecture_Click(object sender, EventArgs e)
        {
            LoadForm(new LeactureManage());
        }
    }
}


