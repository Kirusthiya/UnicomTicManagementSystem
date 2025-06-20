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


        public string GenerateUserID(string role)
        {
            string prefix;
            switch (role)
            {
                case "Admin":
                    prefix = "A";
                    break;
                case "Student":
                    prefix = "S";
                    break;
                case "Staff":
                    prefix = "Sf";
                    break;
                case "Lecturer":
                    prefix = "L";
                    break;
                default:
                    prefix = "U";
                    break;
            }

            Random rand = new Random();
            int number = rand.Next(100000, 999999);
            return prefix + number;
        }

        private void UserLoginCreate_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "Email address";
            txtPassword.Text = "Password";
            txtConfirmpassword.Text = "Confirm password";


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
            else if (!text.Contains("@") && text != "Email address")
            {
                txtUsername.Text = text + "@gmail.com";
            }


        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";

            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";

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

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmpassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            // Input validations
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(role))
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

            // Ensure email ends with @gmail.com
            if (!Regex.IsMatch(username, @"^[^@\s]+@gmail\.com$"))
            {
                if (!username.Contains("@"))
                    username += "@gmail.com";
                else
                {
                    MessageBox.Show("Username must be a valid Gmail address.");
                    return;
                }
            }

            // Generate user ID
            string userId = GenerateUserID(role);
            lblUserID.Text = $"Generated ID: {userId}";

            // Create user object
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
      
    }
}
       
        


