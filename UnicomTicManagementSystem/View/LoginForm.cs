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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UnicomTicManagementSystem.View
{
    public partial class LoginForm : Form
    {
        private readonly LoginController loginController = new LoginController();
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
        public void ClearForm()
        {
            ForgotPanel.Visible = false;
            btnLogin.Visible = true;
            txtForgotUsername.Clear();
            txtForgotPass.Clear();
            txtForcotConPass.Clear();
        }
        public LoginForm()
        {
            InitializeComponent();
            ForgotPanel.Visible = false;
        }
        private async Task LoginUserAsync(string username, string password)
        {
            var user = await loginController.AuthenticateUserAsync(username, password);
            if (user != null)
            {
                MessageBox.Show("Logged in successfully with new password!");

                if (user.Role == "Admin")
                {
                    LoadForm(new AdminMenuForm()); 
                }
                else if (user.Role == "Student")
                {
                    LoadForm(new StudentViewForm());
                }
                else if (user.Role == "Lecturer")
                {
                    LoadForm(new LectureMenuForm(user.UserID));
                }
                else if (user.Role == "Staff")
                {
                    LoadForm(new StaffMenuForm());
                }

                
            }
            else
            {
                MessageBox.Show("Login failed.");
            }
        }
            
        
       
        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "Enter username";
            txtPassword.Text = "Password";
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


        }
      

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
           

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsename_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtUsename_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true; // hide password chars
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false; // show text
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Enter username")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            string text = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                txtUserName.Text = "Enter username";
                txtUserName.ForeColor = Color.Gray;
            }
            else if (!text.EndsWith("@gmail.com") && text != "Enter username")
            {
                txtUserName.Text = text + "@gmail.com";
                txtUserName.ForeColor = Color.Black;
            }
        }
        public string currentForgotUser;
        private void btnForgotPassword_Click_1(object sender, EventArgs e)
        {
      
            currentForgotUser = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(currentForgotUser) || currentForgotUser == "Enter username")
            {
                MessageBox.Show("Please enter your username to reset password.");
                return;
            }

            if (!currentForgotUser.EndsWith("@gmail.com"))
            {
                currentForgotUser += "@gmail.com";
            }

            ForgotPanel.Visible = true;
            btnLogin.Visible = false;
        }
        

        private void txtForgotUsername_Enter(object sender, EventArgs e)
        {
            if (txtForgotUsername.Text == "Username")
            {
                txtForgotUsername.Text = "";
                txtForgotUsername.ForeColor = Color.Black;
            }
        }

        private void txtForgotUsername_Leave(object sender, EventArgs e)
        {
            string text = txtForgotUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                txtForgotUsername.Text = "Username";
                txtForgotUsername.ForeColor = Color.Gray;
            }
            else if (!text.EndsWith("@gmail.com") && text != "Username")
            {
                txtForgotUsername.Text = text + "@gmail.com";
                txtForgotUsername.ForeColor = Color.Black;
            }
        }

        private void txtForgotPass_Enter(object sender, EventArgs e)
        {
            if (txtForgotPass.Text == "Password")
            {
                txtForgotPass.Text = "";
                txtForgotPass.ForeColor = Color.Black;
                txtForgotPass.UseSystemPasswordChar = true; // hide password chars
            }
        }

        private void txtForgotPass_Leave(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtForgotPass.Text))
            {
                txtForgotPass.Text = "Password";
                txtForgotPass.ForeColor = Color.Gray;
                txtForgotPass.UseSystemPasswordChar = false; // show text
            }
        }

        private void txtForcotConPass_Enter(object sender, EventArgs e)
        {
            if (txtForcotConPass.Text=="Confirm password")
            {
                txtForcotConPass.Text = "";
                txtForcotConPass.ForeColor = Color.Black;
                txtForcotConPass.UseSystemPasswordChar= true;
            }
        }

        private  void txtForcotConPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtForcotConPass.Text))
            {
                txtForcotConPass.Text = "Confirm password";
                txtForcotConPass.ForeColor = Color.Gray;
                txtForcotConPass.UseSystemPasswordChar = false;
            }
        }
     
        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)
                || username == "Enter username" || password == "Password")
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (!username.EndsWith("@gmail.com"))
            {
                username += "@gmail.com";
            }

            var user = await loginController.AuthenticateUserAsync(username, password);
            if (user != null)
            {
                MessageBox.Show("Login successful!");

                if (user.Role == "Admin")
                    LoadForm(new AdminMenuForm());
                else if (user.Role == "Student")
                    LoadForm(new StudentViewForm());
                else if (user.Role == "Lecturer")
                    LoadForm(new LectureMenuForm(user.UserID));
                else if (user.Role == "Staff")
                    LoadForm(new StaffMenuForm());

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private async void btnChange_Click_1(object sender, EventArgs e)
        {
            string username = currentForgotUser;
            string newPassword = txtForgotPass.Text.Trim();
            string confirmPassword = txtForcotConPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Both password fields are required.");
                return;
            }

            if (newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            bool updated = await loginController.UpdatePasswordAsync(username, newPassword);
            if (updated)
            {
                MessageBox.Show("Password changed successfully!");

                ClearForm();
                await LoginUserAsync(username, newPassword);
            }
            else
            {
                MessageBox.Show("Failed to update password.");
            }
        }

    }
    
}
