using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTicManagementSystem.View
{
    public partial class StaffMenuForm : Form
    {

        public StaffMenuForm()
        {
            InitializeComponent();
        }
        public void LoadForm(Form form)
        {

            foreach (Control ctrl in panel1.Controls)
            {
                ctrl.Dispose();
            }
            panel1.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel1.Controls.Add(form);
            form.Show();

        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
           LoadForm(new StudentManege());

        }

        private void btneditMark_Click(object sender, EventArgs e)
        {
            LoadForm(new Form1());
        }

        private void btnAddLecture_Click(object sender, EventArgs e)
        {
            LoadForm(new LeactureManage());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new LoginForm());

            }
          
        }
    }
}
