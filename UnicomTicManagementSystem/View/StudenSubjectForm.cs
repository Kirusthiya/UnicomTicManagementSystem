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
    public partial class StudenSubjectForm : Form
    {
        private readonly StudentSubjectConteroller controller = new StudentSubjectConteroller();
        private List<StudentSubject> studentSubjectList = new List<StudentSubject>();
        private int selectedID = 0;
        public StudenSubjectForm()
        {
            InitializeComponent();
            LoadFormData();
        }
        private async void LoadFormData()
        {
            try
            {
                // Load StudentSubjects to Grid
                studentSubjectList = await controller.GetAllStudentSubjectsAsync();
                dgvStudentSubject.DataSource = studentSubjectList;

                // Load Students (Users with Role = 'Student')
                var users = await new UserController().GetAllUsersAsync();
                var students = users.Where(u => u.Role == "Student").ToList();
                cmbUserId.DataSource = students;
                cmbUserId.DisplayMember = "Name";
                cmbUserId.ValueMember = "UserID";

                // Load Subjects
                var subjects = await new SubjectController().GetAllSubjectsAsync();
                cmbsubjectID.DataSource = subjects;
                cmbsubjectID.DisplayMember = "SubjectName";
                cmbsubjectID.ValueMember = "SubjectID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form data: " + ex.Message);
            }
        }
        private void ClearInputs()
        {
            cmbsubjectID.SelectedIndex = -1;
            cmbUserId.SelectedIndex = -1;
            selectedID = 0;
        }
     

      

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbUserId.SelectedIndex == -1 || cmbsubjectID.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select both Student and Subject.");
                    return;
                }

                var newRecord = new StudentSubject
                {
                    UserID = cmbUserId.SelectedValue.ToString(),
                    SubjectID = Convert.ToInt32(cmbsubjectID.SelectedValue)
                };

                bool result = await controller.AddStudentSubjectAsync(newRecord);
                MessageBox.Show(result ? "Added successfully!" : "Failed to add.");
                LoadFormData();
                ClearInputs();
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
                if (selectedID == 0)
                {
                    MessageBox.Show("Select a row to delete.");
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bool deleted = await controller.DeleteStudentSubjectAsync(selectedID);
                    MessageBox.Show(deleted ? "Deleted." : "Delete failed.");
                    LoadFormData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        private  void textBox4_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void dgvStudentSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvStudentSubject.Rows.Count)
                {
                    var row = dgvStudentSubject.Rows[e.RowIndex];
                    selectedID = Convert.ToInt32(row.Cells["ID"].Value);
                    cmbUserId.SelectedValue = row.Cells["UserID"].Value.ToString();
                    cmbsubjectID.SelectedValue = Convert.ToInt32(row.Cells["SubjectID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select Error: " + ex.Message);
            }

        }
    

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedID == 0)
                {
                    MessageBox.Show("Please select a record to update.");
                    return;
                }

                var updated = new StudentSubject
                {
                    ID = selectedID,
                    UserID = cmbUserId.SelectedValue.ToString(),
                    SubjectID = Convert.ToInt32(cmbsubjectID.SelectedValue)
                };

                bool result = await controller.UpdateStudentSubjectAsync(updated);
                MessageBox.Show(result ? "Updated successfully!" : "Update failed.");
                LoadFormData();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
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

        private async void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    LoadFormData(); // reload all
                    return;
                }

                var result = await controller.SearchByStudentNameAsync(name);
                dgvStudentSubject.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
            }
        }
    }
}
