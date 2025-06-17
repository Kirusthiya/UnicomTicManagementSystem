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
using UnicomTicManagementSystem.View;

namespace UnicomTicManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly MarkController markController = new MarkController();
        private readonly ExamController examController = new ExamController();
        private readonly SubjectController subjectController = new SubjectController();
        private readonly UserController userController = new UserController();

        public Form1()
        {
            InitializeComponent();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadExamsAsync();
            await LoadSubjectsAsync();
            await LoadStudentsAsync();
            await LoadMarksAsync();
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

        private async Task LoadStudentsAsync()
        {
            var students = await userController.GetAllUsersAsync(); // assumes method exists
            cmbstudent.DataSource = students;
            cmbstudent.DisplayMember = "Name";
            cmbstudent.ValueMember = "UserID";
        }

        private async Task LoadMarksAsync()
        {
            var marks = await markController.GetAllMarksAsync();
            dgvMark.DataSource = marks;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var mark = new Mark
            {
                UserID = cmbstudent.SelectedValue.ToString(),  // UserID
                ExamID = Convert.ToInt32(cmbExam.SelectedValue),
                SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                Socre = int.TryParse(txtScore.Text, out int score) ? score : 0
            };

            var success = await markController.AddMarkAsync(mark);
            MessageBox.Show(success ? "Mark added." : "Failed to add mark.");
            await LoadMarksAsync();
        }

        private  async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMark.Text, out int markId))
            {
                var mark = new Mark
                {
                    MarkID = markId,
                    Socre = int.TryParse(txtScore.Text, out int score) ? score : 0
                };

                var success = await markController.UpdateMarkAsync(mark);
                MessageBox.Show(success ? "Mark updated." : "Update failed.");
                await LoadMarksAsync();
            }
            else
            {
                MessageBox.Show("Please select a mark from the table first.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMark.Text, out int markId))
            {
                var success = await markController.DeleteMarkAsync(markId);
                MessageBox.Show(success ? "Mark deleted." : "Delete failed.");
                await LoadMarksAsync();
            }
        }

        private void dgvMark_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMark.Rows[e.RowIndex].DataBoundItem is Mark mark)
            {
                txtMark.Text = mark.MarkID.ToString();
                txtScore.Text = mark.Socre.ToString();
                cmbstudent.SelectedValue = mark.UserID;
                cmbExam.SelectedValue = mark.ExamID;
                cmbSubject.SelectedValue = mark.SubjectID;
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string name = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                await LoadMarksAsync(); // show all
            }
            else
            {
                var results = await markController.SearchMarksByStudentNameAsync(name);
                dgvMark.DataSource = results;
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
