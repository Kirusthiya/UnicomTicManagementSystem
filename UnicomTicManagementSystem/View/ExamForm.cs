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
    public partial class ExamForm : Form
    {
        private ExamController examController = new ExamController();
        private SubjectController subjectController = new SubjectController();
        private CourseController courseController = new CourseController();

        public ExamForm()
        {
            InitializeComponent();
            LoadSubjects();
            LoadCourses();
            LoadExamData();
        }
        private async void LoadSubjects()
        {
            var subjects = await subjectController.GetAllSubjectsAsync();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private async void LoadCourses()
        {
            var courses = await courseController.GetAllCoursesAsync();
            cmbCourse.DataSource = courses;
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
        }

        private async void LoadExamData()
        {
            var exams = await examController.GetAllExamsAsync();
            dgvExam.DataSource = exams;
        }

       
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExamname.Text))
            {
                MessageBox.Show("Please enter exam name.");
                return;
            }

            var exam = new Exam
            {
                ExamName = txtExamname.Text.Trim(),
                SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
            };

            bool success = await examController.AddExamAsync(exam);
            MessageBox.Show(success ? "Exam added successfully!" : "Failed to add exam.");
            LoadExamData();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtExamId.Text, out int examId))
            {
                MessageBox.Show("Invalid Exam ID.");
                return;
            }

            var exam = new Exam
            {
                ExamID = examId,
                ExamName = txtExamname.Text.Trim(),
                SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
            };

            bool success = await examController.UpdateExamAsync(exam);
            MessageBox.Show(success ? "Exam updated." : "Update failed.");
            LoadExamData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtExamId.Text, out int examId))
            {
                MessageBox.Show("Enter valid Exam ID to delete.");
                return;
            }

            bool success = await examController.DeleteExamAsync(examId);
            MessageBox.Show(success ? "Exam deleted." : "Delete failed.");
            LoadExamData();
        }

        private void dgvExam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtExamId.Text, out int examId))
            {
                MessageBox.Show("Enter valid Exam ID.");
                return;
            }

            var exam = await examController.GetExamByIdAsync(examId);
            if (exam != null)
            {
                txtExamname.Text = exam.ExamName;
                cmbSubject.SelectedValue = exam.SubjectID;
                cmbCourse.SelectedValue = exam.CourseID;
            }
            else
            {
                MessageBox.Show("Exam not found.");
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
