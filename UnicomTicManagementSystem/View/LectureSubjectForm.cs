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

namespace UnicomTicManagementSystem.View
{
    public partial class LectureSubjectForm : Form
    {
        private readonly LecturerSubjectController controller = new LecturerSubjectController();
        public LectureSubjectForm()
        {
            InitializeComponent();
            _ = LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            try
            {
                var list = await controller.GetAllLecturerSubjectsAsync();
                dgvLectureSubject.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserId.Text) || string.IsNullOrWhiteSpace(txtSubjectId.Text))
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                string userId = txtUserId.Text.Trim();
                if (!int.TryParse(txtSubjectId.Text.Trim(), out int subjectId))
                {
                    MessageBox.Show("Invalid Subject ID.");
                    return;
                }

                bool result = await controller.AddLecturerSubjectAsync(userId, subjectId);
                if (result)
                {
                    MessageBox.Show("Lecturer subject added successfully.");
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to add lecturer subject.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLectureSubject.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row to delete.");
                    return;
                }

                int id = Convert.ToInt32(dgvLectureSubject.SelectedRows[0].Cells["LecturerSubjectID"].Value);
                var confirm = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    bool result = await controller.DeleteLecturerSubjectAsync(id);
                    if (result)
                    {
                        MessageBox.Show("Deleted successfully.");
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting: " + ex.Message);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    await LoadDataAsync();
                    return;
                }

                var list = await controller.SearchByLecturerNameAsync(name);
                dgvLectureSubject.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }
        private void ClearInputs()
        {
            txtUserId.Clear();
            txtSubjectId.Clear();
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
