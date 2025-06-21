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
    public partial class MarkForm : Form
    {
        private readonly MarkController markController = new MarkController();
        private readonly UserController userController = new UserController();
        private readonly ExamController examController = new ExamController();
        private readonly SubjectController subjectController = new SubjectController();

        public MarkForm()
        {
            InitializeComponent();
            
        }
        private async Task LoadStudentsAsync()
        {
            var students = await userController.GetAllStudentsAsync();
            cmbstudent.DataSource = students;
            cmbstudent.DisplayMember = "Name";
            cmbstudent.ValueMember = "UserID";
        }
        private async Task LoadExamsAsync()
        {
            var exams = await examController.GetAllExamsAsync();
            cmbExam.DataSource = exams;
            cmbExam.DisplayMember = "ExamName";
            cmbExam.ValueMember = "ExamID";
        }
        private async Task LoadSubjectsAsync()
        {
            var subjects = await subjectController.GetAllSubjectsAsync();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private async Task LoadMarksAsync(string studentUserId = null)
        {
            List<Mark> marks;
            if (string.IsNullOrEmpty(studentUserId))
                marks = await markController.GetAllMarksAsync();
            else
                marks = await markController.GetMarksByUserIDAsync(studentUserId);

            dgvMark.DataSource = marks;
            dgvMark.ClearSelection();
        }
        private void dgvMark_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMark.Rows[e.RowIndex].DataBoundItem is Mark mark)
            {
                txtMark.Text = mark.MarkID.ToString();
                txtScore.Text = mark.Score.ToString();

                cmbstudent.SelectedValue = mark.UserID;
                cmbExam.SelectedValue = mark.ExamID;
                cmbSubject.SelectedValue = mark.SubjectID;
            }
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                await LoadMarksAsync();
            }
            else
            {
                var results = await markController.SearchMarksByStudentNameAsync(searchText);
                dgvMark.DataSource = results;
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMark.Text, out int markId))
            {
                bool success = await markController.DeleteMarkAsync(markId);
                MessageBox.Show(success ? "Mark deleted." : "Delete failed.");
                await LoadMarksAsync();
            }
            else
            {
                MessageBox.Show("Select a mark from the table.");
            }
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMark.Text, out int markId) && int.TryParse(txtScore.Text, out int score))
            {
                var mark = new Mark
                {
                    MarkID = markId,
                    Score = score
                };

                bool success = await markController.UpdateMarkAsync(mark);
                MessageBox.Show(success ? "Mark updated." : "Update failed.");
                await LoadMarksAsync();
            }
            else
            {
                MessageBox.Show("Select a mark from the table and enter a valid score.");
            }
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtScore.Text, out int score))
            {
                if (cmbstudent.SelectedValue == null || cmbExam.SelectedValue == null || cmbSubject.SelectedValue == null)
                {
                    MessageBox.Show("Please select student, exam and subject.");
                    return;
                }
                var mark = new Mark
                {
                    UserID = cmbstudent.SelectedValue.ToString(),
                    ExamID = (int)cmbExam.SelectedValue,
                    SubjectID = (int)cmbSubject.SelectedValue,
                    Score = score
                };

                bool success = await markController.AddMarkAsync(mark);
                MessageBox.Show(success ? "Mark added." : "Failed to add mark.");
                await LoadMarksAsync();
            }
            else
            {
                MessageBox.Show("Please enter a valid score.");
            }
        }
        private async void MarkForm_Load(object sender, EventArgs e)
        {
            await LoadStudentsAsync();
            await LoadExamsAsync();
            await LoadSubjectsAsync();
            await LoadMarksAsync();
        }

        private void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
