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
            cmbRole.Items.Add("Admin"); 
            cmbRole.Items.Add("Staff"); 
            cmbRole.Items.Add("Lecture"); 
            cmbRole.Items.Add("Student"); 
            
        }

       
        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string selectedRole = cmbRole.SelectedItem.ToString();

           
            LoginForm loginForm = new LoginForm(selectedRole);
            loginForm.TopLevel = false;
            loginForm.FormBorderStyle = FormBorderStyle.None;
            loginForm.Dock = DockStyle.Fill;

            this.Controls.Clear();            
            this.Controls.Add(loginForm);     
            loginForm.Show();                 
        }
    }
       
    
}
