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
    public partial class LectureSubjectForm : Form
    {

        private readonly LecturerSubjectController controller = new LecturerSubjectController();
        private List<LecturerSubject> lecturerSubjectList = new List<LecturerSubject>();

        public LectureSubjectForm()
        {
            InitializeComponent();
            LoadData();

        }
      

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private async void LoadData()
        {
            try
            {
                // Load table
                lecturerSubjectList = await controller.GetAllLecturerSubjectsAsync();
                dgvLectureSubject.DataSource = lecturerSubjectList;

                // Load lecturers
                var users = await new UserController().GetAllUsersAsync();
                var lecturers = users.Where(u => u.Role == "Lecturer").ToList();
                cmbUserId.DataSource = lecturers;
                cmbUserId.DisplayMember = "Name";
                cmbUserId.ValueMember = "UserID";

                // Load subjects
                var subjects = await new SubjectController().GetAllSubjectsAsync();
                cmbSubject.DataSource = subjects;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubjectID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var lecturerSubject = new LecturerSubject
                {
                    UserID = cmbUserId.SelectedValue.ToString(),
                    SubjectID = Convert.ToInt32(cmbSubject.SelectedValue)
                };

                bool result = await controller.AddLecturerSubjectAsync(lecturerSubject);
                MessageBox.Show(result ? "Lecturer subject added." : "Already exists or failed to add.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLectureSubject.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvLectureSubject.CurrentRow.Cells["ID"].Value);
                    bool result = await controller.DeleteLecturerSubjectAsync(id);
                    MessageBox.Show(result ? "Deleted successfully." : "Delete failed.");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string search = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(search))
                {
                    LoadData();
                    return;
                }

                var result = await controller.SearchByLecturerNameAsync(search);
                dgvLectureSubject.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
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

        private void dgvLectureSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvLectureSubject.Rows.Count)
                {
                    var row = dgvLectureSubject.Rows[e.RowIndex];
                    cmbUserId.SelectedValue = row.Cells["UserID"].Value.ToString();
                    cmbSubject.SelectedValue = Convert.ToInt32(row.Cells["SubjectID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select Error: " + ex.Message);
            }

        }

        private async void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLectureSubject.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvLectureSubject.CurrentRow.Cells["ID"].Value);
                    var lecturerSubject = new LecturerSubject
                    {
                        ID = id,
                        UserID = cmbUserId.SelectedValue.ToString(),
                        SubjectID = Convert.ToInt32(cmbSubject.SelectedValue)
                    };

                    bool result = await controller.UpdateLecturerSubjectAsync(lecturerSubject);
                    MessageBox.Show(result ? "Updated successfully." : "Update failed.");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }
    }
    
}
