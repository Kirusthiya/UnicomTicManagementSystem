using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private readonly ExamController examController = new ExamController();
        private string selectedFileName = "";
        private int selectedExamId = -1;


        public ExamForm()
        {
            InitializeComponent();
            LoadFormData();
        }
        private async void LoadFormData()
        {
            await LoadCourses();
            await LoadSubjects();
            await LoadExamTable();
        }

        private async Task LoadCourses()
        {
            var courseDict = await examController.GetCoursesAsync();
            cmbCourse.DataSource = new BindingSource(courseDict, null);
            cmbCourse.DisplayMember = "Value";
            cmbCourse.ValueMember = "Key";
        }

        private async Task LoadSubjects()
        {
            var subjectDict = await examController.GetSubjectsAsync();
            cmbSubject.DataSource = new BindingSource(subjectDict, null);
            cmbSubject.DisplayMember = "Value";
            cmbSubject.ValueMember = "Key";
        }

        private async Task LoadExamTable()
        {
            var exams = await examController.GetAllExamsAsync();
            dgvExam.DataSource = exams;
            dgvExam.Columns["SubjectID"].Visible = false;
            dgvExam.Columns["CourseID"].Visible = false;
        }


        private void ClearForm()
        {
            txtExamname.Clear();
            txtSearch.Clear();
            lblfile.Text = "No file selected";
            selectedFileName = "";
            selectedExamId = -1;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExamname.Text))
            {
                MessageBox.Show("Exam name is required.");
                return;
            }

            var exam = new Exam
            {
                ExamName = txtExamname.Text.Trim(),
                SubjectID = (int)((KeyValuePair<int, string>)cmbSubject.SelectedItem).Key,
                CourseID = (int)((KeyValuePair<int, string>)cmbCourse.SelectedItem).Key,
                FileName = selectedFileName
            };

            bool success = await examController.AddExamAsync(exam);
            if (success)
            {
                MessageBox.Show("Exam added successfully.");
                await LoadExamTable();
                ClearForm();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedExamId == -1)
            {
                MessageBox.Show("Select an exam from the table first.");
                return;
            }

            var exam = new Exam
            {
                ExamID = selectedExamId,
                ExamName = txtExamname.Text.Trim(),
                SubjectID = (int)((KeyValuePair<int, string>)cmbSubject.SelectedItem).Key,
                CourseID = (int)((KeyValuePair<int, string>)cmbCourse.SelectedItem).Key,
                FileName = selectedFileName
            };

            bool success = await examController.UpdateExamAsync(exam);
            if (success)
            {
                MessageBox.Show("Exam updated successfully.");
                await LoadExamTable();
                ClearForm();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {

            if (selectedExamId == -1)
            {
                MessageBox.Show("Select an exam from the table first.");
                return;
            }

            bool success = await examController.DeleteExamAsync(selectedExamId);
            if (success)
            {
                MessageBox.Show("Exam deleted.");
                await LoadExamTable();
                ClearForm();
            }
        }

        private void dgvExam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedExamId = Convert.ToInt32(dgvExam.Rows[e.RowIndex].Cells["ExamID"].Value);
                txtExamname.Text = dgvExam.Rows[e.RowIndex].Cells["ExamName"].Value.ToString();
                cmbSubject.Text = dgvExam.Rows[e.RowIndex].Cells["SubjectName"].Value.ToString();
                cmbCourse.Text = dgvExam.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();
                selectedFileName = dgvExam.Rows[e.RowIndex].Cells["FileName"].Value.ToString();
                lblfile.Text = selectedFileName;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            var result = await examController.SearchExamAsync(keyword);
            dgvExam.DataSource = result;
        }
     

       

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedFileName = Path.GetFileName(ofd.FileName);
                    lblfile.Text = selectedFileName;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
