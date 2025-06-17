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
    public partial class SubjectForm : Form
    {
        private SubjectController subjectController = new SubjectController();
        private CourseController CourseController = new CourseController();
        private int SelectedSubjectID=-1;
        

        public SubjectForm()
        {
            InitializeComponent();
        }

        private async void SubjectForm_Load(object sender, EventArgs e)
        {
            await LoadSubjectsAsyns();
            await LoadCoursesAsync();
            await LoadSubjectsAsyns();
            txtSubject.Clear();
        }
        private async Task LoadCoursesAsync()
        {
            try
            {
                var courses = await CourseController.GetAllCoursesAsync();
                cmbCourse.DataSource = courses;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseID";
            }
            catch
            {
                MessageBox.Show("Error Loading Course ");
            }
          
        }
        private async Task LoadSubjectsAsyns()
        {
            var subjects = await subjectController.GetAllSubjectsAsync();
            dataGridView1.DataSource = subjects;
        }
      
        private void ClearForm()
        {
            txtSubject.Clear();
            cmbCourse.SelectedIndex = 0;
            SelectedSubjectID = -1;
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    SelectedSubjectID = Convert.ToInt32(row.Cells["SubjectID"].Value);
                    txtSubject.Text = row.Cells["SubjectName"].Value.ToString();
                    cmbCourse.SelectedValue = Convert.ToInt32(row.Cells["CourseID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting subject: " + ex.Message);
            }
        }

        private async void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSubject.Text.Trim();
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    await LoadSubjectsAsyns();
                    return;
                }

                var results = await subjectController.SearchSubjectsAsync(searchTerm);
                dataGridView1.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching subject: " + ex.Message);
            }
        }

        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (SelectedSubjectID == -1)
            {
                MessageBox.Show("Please select a subject to update.");
                return;
            }

            try
            {
                var subject = new Subject
                {
                    SubjectID = SelectedSubjectID,
                    SubjectName = txtSubject.Text.Trim(),
                    CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
                };

                bool success = await subjectController.UpdateSubjectAsync(subject);
                if (success)
                {
                    MessageBox.Show("Subject updated successfully.");
                    ClearForm();
                    await LoadSubjectsAsyns();
                }
                else
                {
                    MessageBox.Show("Failed to update subject.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating subject: " + ex.Message);
            }


        }

        private async void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (SelectedSubjectID == -1)
            {
                MessageBox.Show("Please select a subject to delete.");
                return;
            }

            try
            {
                var confirm = MessageBox.Show("Are you sure you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    bool success = await subjectController.DeleteSubjectAsync(SelectedSubjectID);
                    if (success)
                    {
                        MessageBox.Show("Subject deleted successfully.");
                        ClearForm();
                        await LoadSubjectsAsyns();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete subject.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting subject: " + ex.Message);
            }
        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please enter subject name.");
                return;
            }

            try
            {
                var subject = new Subject
                {
                    SubjectName = txtSubject.Text.Trim(),
                    CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
                };

                bool success = await subjectController.AddSubjectAsync(subject);
                if (success)
                {
                    MessageBox.Show("Subject added successfully.");
                    ClearForm();
                    await LoadSubjectsAsyns();
                }
                else
                {
                    MessageBox.Show("Failed to add subject.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding subject: " + ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
