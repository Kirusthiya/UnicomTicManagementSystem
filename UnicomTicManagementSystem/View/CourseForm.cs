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
    public partial class CourseForm : Form
    {

        private CourseController controller = new CourseController();
        private int selectedCourseID = -1;


        public CourseForm()
        {
            InitializeComponent();
            LoadCoursesAsync();
           
        }
        private async void LoadCoursesAsync()
        {
            var courses = await controller.GetAllCoursesAsync();
            if(courses != null)
            {
                dataGridView1.DataSource = courses;

            }
            else
            {
                MessageBox.Show("No courses found.");
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCourseID == -1)
            {
                MessageBox.Show("Please select a course to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this course?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                bool result = await controller.DeleteCourseAsync(selectedCourseID);
                MessageBox.Show(result ? "Course deleted." : "Failed to delete course.");
                LoadCoursesAsync();
                txtCourse.Clear();
                selectedCourseID = -1;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                MessageBox.Show("Please enter a course name.");
                return;
            }

            var course = new Course { CourseName = txtCourse.Text.Trim() };

            bool result = await controller.AddCourseAsync(course);
            MessageBox.Show(result ? "Course added successfully." : "Failed to add course.");
            LoadCoursesAsync();
            txtCourse.Clear();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCourseID == -1)
            {
                MessageBox.Show("Please select a course to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                MessageBox.Show("Course name cannot be empty.");
                return;
            }

            var course = new Course { CourseID = selectedCourseID, CourseName = txtCourse.Text.Trim() };

            bool result = await controller.UpdateCourseAsync(course);
            MessageBox.Show(result ? "Course updated successfully." : "Failed to update course.");
            LoadCoursesAsync();
            txtCourse.Clear();
            selectedCourseID = -1;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCourse.Text.Trim(), out int courseId))
            {
                MessageBox.Show("Enter a valid Course ID to search.");
                return;
            }

            var course = await controller.GetCourseByIdAsync(courseId);
            if (course != null)
            {
                txtCourse.Text = course.CourseName;
                selectedCourseID = course.CourseID;
                MessageBox.Show("Course found. You can update or delete.");
            }
            else
            {
                MessageBox.Show("Course not found.");
                selectedCourseID = -1;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedCourseID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CourseID"].Value);
                txtCourse.Text = dataGridView1.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();
            }
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new AdminMenuForm());

            }
        }

    }
}
