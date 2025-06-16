using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Model;

namespace UnicomTicManagementSystem.View
{
    public partial class UserCreateForm : Form
    {
        public UserCreateForm()
        {
            InitializeComponent();
            LoadForm(new MainDashboad());
        }

        public void LoadForm(object formObj)
        {
            if (this.panelMain.Controls.Count > 0)
            {
                this.panelMain.Controls.RemoveAt(0);
            }

            Form form = formObj as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(form);
            this.panelMain.Tag = form;
            form.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            LoadForm(new AdminMenuForm());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLecture_Click(object sender, EventArgs e)
        {
            LoadForm(new LoginForm());

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            LoadForm(new StudentViewForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            LoadForm(new StaffMenuForm());
        }
    }
}
