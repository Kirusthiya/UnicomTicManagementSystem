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
        private readonly Lecturecontroller lecturerController;
    
        public LeactureManage()
        {
            InitializeComponent();
            lecturerController = new Lecturecontroller();

        }
       

        private async Task LoadLecturers()
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
        private void ClearForm()
        {
            txtUserId.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtSalary.Text = "";
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            rdoOthers.Checked = false;
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNo.Text) ||
                string.IsNullOrWhiteSpace(txtSalary.Text) ||
                string.IsNullOrWhiteSpace(GetSelectedGender()))
            {
                MessageBox.Show("All fields are required.");
                return false;
            }

            if (!decimal.TryParse(txtSalary.Text, out _))
            {
                MessageBox.Show("Salary must be a valid decimal.");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNo.Text, @"^07\d{8}$"))
            {
                MessageBox.Show("Phone number must start with 07 and be 10 digits.");
                return false;
            }

            return true;
        }


        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var lecturer = new Lecture
            {
                UserID = txtUserId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                PhoneNumber = txtPhoneNo.Text.Trim(),
                Gender = GetSelectedGender(),
                Salary = decimal.Parse(txtSalary.Text.Trim())
            };

            bool success = await lecturerController.AddLecturerAsync(lecturer);
            if (success)
            {
                MessageBox.Show("Lecturer added successfully.");
                await LoadLecturers();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to add lecturer.");
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
                PhoneNumber = txtPhoneNo.Text.Trim(),
                Gender = GetSelectedGender(),
                Salary = decimal.Parse(txtSalary.Text.Trim())
            };

            bool success = await lecturerController.UpdateLecturerAsync(lecturer);
            if (success)
            {
                MessageBox.Show("Lecturer updated.");
                await LoadLecturers();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Update failed.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                MessageBox.Show("Enter User ID to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                bool success = await lecturerController.DeleteLecturerAsync(txtUserId.Text.Trim());
                if (success)
                {
                    MessageBox.Show("Lecturer deleted.");
                    await LoadLecturers();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Delete failed.");
                }
            }
        }

        private void dgvLecturer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvLecturer.Rows[e.RowIndex];
                txtUserId.Text = row.Cells["UserID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtPhoneNo.Text = row.Cells["PhoneNumber"].Value.ToString();
                txtSalary.Text = row.Cells["Salary"].Value.ToString();

                string gender = row.Cells["Gender"].Value.ToString();
                if (gender == "Male") rdoMale.Checked = true;
                else if (gender == "Female") rdoFemale.Checked = true;
                else if (gender == "Other") rdoOthers.Checked = true;
            }
        }
      
        

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
           
        }
        
      

    

        private async void button1_Click(object sender, EventArgs e)
        {
            string name = txtsearch.Text.Trim();
            var lecturers = await lecturerController.GetLecturerByNameAsync(name);
            dgvLecturer.DataSource = lecturers;
        }

        private async void LeactureManage_Load(object sender, EventArgs e)
        {
            await LoadLecturers();

        }
    }
}
