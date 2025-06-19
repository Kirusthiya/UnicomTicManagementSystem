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
    public partial class StaffManege : Form
    {
        private readonly StaffController staffController = new StaffController();

        public StaffManege()
        {
            InitializeComponent();
            LoadStaffData();
            
        }
        private async void LoadStaffData()
        {
            var staffList = await staffController.GetAllStaffAsync();
            dvgStaff.DataSource = staffList;
        }

        private string GetGender()
        {
            if (rdoMAle.Checked) return "Male";
            if (rdoFemale.Checked) return "Female";
            return "Other";
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^07\d{8}$");
        }

        private void ClearFields()
        {
            txtUserID.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            txtPhoneNo.Clear();
            rdoFemale.Checked = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void LoadForm(Form form)
        {
            // Remove any existing control (and dispose it properly)
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
        
        private void btnlogout_Click_2(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new AdminMenuForm());

            }
        }

        private async void btnDelete_Click_1(object sender, EventArgs e)
        {
            string userId = txtUserID.Text;
            if (string.IsNullOrWhiteSpace(userId))
            {
                MessageBox.Show("Please enter UserID to delete.");
                return;
            }

            bool result = await staffController.DeleteStaffAsync(userId);
            MessageBox.Show(result ? "Staff deleted successfully!" : "Failed to delete staff.");
            LoadStaffData();
            ClearFields();
        }

        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (!IsValidPhoneNumber(txtPhoneNo.Text))
            {
                MessageBox.Show("Phone number must start with 07 and be 10 digits.");
                return;
            }

            var staff = new Staff
            {
                UserID = txtUserID.Text,
                Name = txtName.Text,
                Address = txtAddress.Text,
                Gender = GetGender(),
                Position = txtPosition.Text,
                Salary = double.TryParse(txtSalary.Text, out double sal) ? sal : 0,
                PhoneNumber = txtPhoneNo.Text
            };
            bool result = await staffController.UpdateStaffAsync(staff);
            MessageBox.Show(result ? "Staff updated successfully!" : "Failed to update staff.");
            LoadStaffData();
            ClearFields();

        }

        private void dvgStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvgStaff.CurrentRow != null)
            {
                var row = dvgStaff.CurrentRow;

                txtUserID.Text = row.Cells["UserID"].Value.ToString();
                txtName.Text = row.Cells["StaffName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtPosition.Text = row.Cells["Position"].Value.ToString();
                txtSalary.Text = row.Cells["Salary"].Value.ToString();
                txtPhoneNo.Text = row.Cells["PhoneNumber"].Value.ToString();

                string gender = row.Cells["Gender"].Value.ToString();
                if (gender == "Male")
                    rdoMAle.Checked = true;
                else if (gender == "Female")
                    rdoFemale.Checked = true;
            }
        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (!IsValidPhoneNumber(txtPhoneNo.Text))
            {
                MessageBox.Show("Phone number must start with 07 and be 10 digits.");
                return;
            }

            var staff = new Staff
            {
                UserID = txtUserID.Text,
                Name = txtName.Text,
                Address = txtAddress.Text,
                Gender = GetGender(),
                Position = txtPosition.Text,
                Salary = double.TryParse(txtSalary.Text, out double sal) ? sal : 0,
                PhoneNumber = txtPhoneNo.Text
            };

            bool result = await staffController.AddStaffAsync(staff);
            MessageBox.Show(result ? "Staff added successfully!" : "Failed to add staff.");
            LoadStaffData();
            ClearFields();
        }

        private async void btnSearch_Click_1(object sender, EventArgs e)
        {

            string name = txtName.Text;
            var result = await staffController.SearchStaffByNameAsync(name);
            dvgStaff.DataSource = result;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

