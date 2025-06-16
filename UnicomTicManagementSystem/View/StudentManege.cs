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
using UnicomTicManagementSystem.Model;

namespace UnicomTicManagementSystem.View
{
    public partial class StudentManege : Form
    {
        private StudentController studentController;
        private CourseController courseController;

        public StudentManege()
        {
            InitializeComponent();
            studentController = new StudentController();
            courseController = new CourseController();
        }

        private async void StudentManege_Load(object sender, EventArgs e)
        {
            await LoadCourses();
            await LoadStudents();
        }
        private async Task LoadCourses()
        {
            var courses = await courseController.GetAllCoursesAsync();
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
            cmbCourse.DataSource = courses;
        }

        private async Task LoadStudents()
        {
            var students = await studentController.GetAllStudentsAsync();
            dgvStudent.DataSource = students;
        }
        private string GetSelectedGender()
        {
            if (rdoMale.Checked) return "Male";
            if (rdoFemale.Checked) return "Female";
            if (rdoOther.Checked) return "Other";
            return string.Empty;
        }
        private void ClearForm()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhoneNO.Text = "";
            cmbCourse.SelectedIndex = -1;
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            rdoOther.Checked = false;
            txtUserId.Text = "";
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text) ||
            string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtAddress.Text) ||
            string.IsNullOrWhiteSpace(txtPhoneNO.Text) ||
            string.IsNullOrWhiteSpace(GetSelectedGender()) ||
            cmbCourse.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return false;
            }

            // Phone number validation (starts with 07 and has 10 digits)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNO.Text, @"^07\d{8}$"))
            {
                MessageBox.Show("Phone number must start with 07 and contain exactly 10 digits.");
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

        }

        private void btnlogout_Click_1(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new AdminMenuForm());

            }
        }

        private void txtPhoneNO_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void txtUserId_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click_1(object sender, EventArgs e)
        {
            
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    MessageBox.Show("Please enter User ID to search.");
                    return;
                }
                var userId = txtUserId.Text.Trim();
                if (string.IsNullOrEmpty(userId))
                {
                    MessageBox.Show("Please enter a valid User ID.");
                    return;
                }


                var student = await studentController.GetStudentByUserIdAsync(userId);
                if (student != null)
                {
                    txtName.Text = student.Name;
                    txtAddress.Text = student.Address;
                    txtPhoneNO.Text = student.PhoneNumber;
                    cmbCourse.SelectedValue = student.CourseID;

                    if (student.Gender == "Male") rdoMale.Checked = true;
                    else if (student.Gender == "Female") rdoFemale.Checked = true;
                    else if (student.Gender == "Other") rdoOther.Checked = true;
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
         }

        private async void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Please enter User ID to search.");
                return;
            }
            var userId = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter a valid User ID.");
                return;
            }
            bool success = await studentController.DeleteStudentAsync(userId);
            if (success)
            {
                MessageBox.Show("Student deleted successfully!");
                ClearForm();
                await LoadStudents();
            }
            else
            {
                MessageBox.Show("Failed to delete student.");
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {

            if (!ValidateInput()) return;

            var student = new Student
            {
                UserID = txtUserId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                PhoneNumber = txtPhoneNO.Text.Trim(),
                Gender = GetSelectedGender(),
                CourseID = (int)cmbCourse.SelectedValue
            };

            bool success = await studentController.UpdateStudentAsync(student);
            if (success)
            {
                MessageBox.Show("Student updated successfully!");
                ClearForm();
                await LoadStudents();
            }
            else
            {
                MessageBox.Show("Failed to update student.");
            }
        }

        private void lblCourse_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {

            if (!ValidateInput()) return;

            var student = new Student
            {
                UserID = txtUserId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                PhoneNumber = txtPhoneNO.Text.Trim(),
                Gender = GetSelectedGender(),
                CourseID = (int)cmbCourse.SelectedValue
            };

            bool success = await studentController.AddStudentAsync(student);
            if (success)
            {
                MessageBox.Show("Student added successfully!");
                ClearForm();
                await LoadStudents();
            }
            else
            {
                MessageBox.Show("Failed to add student.");
            }
        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grbGender_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

