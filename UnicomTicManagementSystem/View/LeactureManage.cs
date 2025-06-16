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
    public partial class LeactureManage : Form
    {
        private readonly Lecturecontroller lecturerController = new Lecturecontroller();
        public LeactureManage()
        {
            InitializeComponent();
           
        }
        private async void LeactureManage_Load(object sender, EventArgs e)
        {
            await LoadLecturersAsync();
        }

        private async Task LoadLecturersAsync()
        {
            var lecturers = await lecturerController.GetAllLecturersAsync();
            dgvLecturer.DataSource = lecturers;
        }

        private string GetSelectedGender()
        {
            if (rdoMale.Checked) return "Male";
            if (rdoFemale.Checked) return "Female";
            if (rdoOthers.Checked) return "Other";
            return string.Empty;
        }

        private void SetSelectedGender(string gender)
        {
            rdoMale.Checked = gender == "Male";
            rdoFemale.Checked = gender == "Female";
            rdoOthers.Checked = gender == "Other";
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var lecturer = new Lecture
            {
                UserID = txtUserId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Gender = GetSelectedGender(),
                Salary = decimal.Parse(txtSalary.Text),
                PhoneNumber = txtPhoneNo.Text.Trim()
            };

            bool result = await lecturerController.AddLecturerAsync(lecturer);
            MessageBox.Show(result ? "Lecturer added successfully!" : "Failed to add lecturer.");
            if (result)
            {
                ClearForm();
                await LoadLecturersAsync();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var lecturer = new Lecture
            {
                UserID = txtUserId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Gender = GetSelectedGender(),
                Salary = decimal.Parse(txtSalary.Text),
                PhoneNumber = txtPhoneNo.Text.Trim()
            };

            bool result = await lecturerController.UpdateLecturerAsync(lecturer);
            MessageBox.Show(result ? "Lecturer updated successfully!" : "Failed to update lecturer.");
            if (result)
            {
                ClearForm();
                await LoadLecturersAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            if (string.IsNullOrWhiteSpace(userId))
            {
                MessageBox.Show("Please enter a valid User ID.");
                return;
            }

            bool result = await lecturerController.DeleteLecturerAsync(userId);
            MessageBox.Show(result ? "Lecturer deleted." : "Failed to delete lecturer.");
            if (result)
            {
                ClearForm();
                await LoadLecturersAsync();
            }
        }

        private void dgvLecturer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLecturer.Rows[e.RowIndex];
                txtUserId.Text = row.Cells["UserID"].Value.ToString();
                txtName.Text = row.Cells["LecturerName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                SetSelectedGender(row.Cells["Gender"].Value.ToString());
                txtSalary.Text = row.Cells["Salary"].Value.ToString();
                txtPhoneNo.Text = row.Cells["PhoneNumber"].Value.ToString();
            }
        }
      
        

        private async void txtsearch_TextChanged(object sender, EventArgs e)
        {

            var lecturers = await lecturerController.SearchLecturersByNameAsync(txtsearch.Text.Trim());
            dgvLecturer.DataSource = lecturers;
        }
        private void ClearForm()
        {
            txtUserId.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtPhoneNo.Clear();
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            rdoOthers.Checked = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtSalary.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(GetSelectedGender()))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            if (!Regex.IsMatch(txtPhoneNo.Text, @"^07\d{8}$"))
            {
                MessageBox.Show("Phone number must be 10 digits and start with '07'.");
                return false;
            }

            if (!decimal.TryParse(txtSalary.Text, out _))
            {
                MessageBox.Show("Salary must be a valid number.");
                return false;
            }

            return true;
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
        private void btnlogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new AdminMenuForm());

            }
        }
    
    }
}
