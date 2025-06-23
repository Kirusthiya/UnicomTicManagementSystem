using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Controller;
using UnicomTicManagementSystem.Model;

namespace UnicomTicManagementSystem.View
{
    public partial class UserLoginCreate : Form
    {
        private readonly UserController userController = new UserController();
        public UserLoginCreate()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[] { "Admin", "Staff", "Lecturer", "Student" });
            cmbRole.SelectedIndex = 0;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Email address")
            {
                txtUsername.Text = "";
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            string text = txtUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Email address";
            }
           
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.UseSystemPasswordChar = true;

            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtConfirmpassword_Enter(object sender, EventArgs e)
        {

            if (txtConfirmpassword.Text == "Confirm password")
            {
                txtConfirmpassword.Text = "";
                txtConfirmpassword.UseSystemPasswordChar = true;
            }
        }

        private void txtConfirmpassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmpassword.Text))
            {
                txtConfirmpassword.Text = "Confirm password";
                txtConfirmpassword.UseSystemPasswordChar = true;
            }
        }

        //create user==================================
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmpassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            if (!username.EndsWith("@gmail.com") && username != "Enter username")
            {
                if (!username.Contains("@"))
                {
                    DialogResult result = MessageBox.Show("Username should end with @gmail.com. Do you want to add it automatically?", "Email Format", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        username += "@gmail.com";
                        txtUsername.Text = username;
                        
                    }
                    else
                    {
                        txtUsername.Focus();
                    }

                    return;
                }
                else
                {
                    MessageBox.Show("Username must be a valid Gmail address.");
                    return;
                }
            }


            string userId = userController.GenerateUserID(role);
            lblUserID.Text = $"Generated ID: {userId}";

            var user = new User
            {
                UserID = userId,
                UserName = username,
                Password = password,
                Role = role
            };

            bool success = await userController.CreateUserAsync(user, confirmPassword);
            if (success)
            {
                MessageBox.Show("User created successfully!");
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to create user.");
            }
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmpassword.Clear();
            cmbRole.SelectedIndex = 0;
        }

        private void UserLoginCreate_Load_1(object sender, EventArgs e)
        {
            txtUsername.Text = "Email address";
            txtPassword.Text = "Password";
            txtConfirmpassword.Text = "Confirm password";
        }
    }
}
       
        


