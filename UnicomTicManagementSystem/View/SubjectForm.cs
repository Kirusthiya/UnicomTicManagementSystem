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
        private readonly SubjectController subjectController = new SubjectController();
        private readonly CourseController courseController = new CourseController();
        private List<Subject> subjectList = new List<Subject>();
        public SubjectForm()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                // Load DataGridView
                subjectList = await subjectController.GetAllSubjectsAsync();
                dataGridView1.DataSource = subjectList;

                // Load Courses into ComboBox
                var courses = await courseController.GetAllCoursesAsync();
                cmbCourse.DataSource = courses;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
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
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    var row = dataGridView1.Rows[e.RowIndex];
                    txtSubject.Text = row.Cells["SubjectName"].Value.ToString();
                    cmbCourse.SelectedValue = Convert.ToInt32(row.Cells["CourseID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selection Error: " + ex.Message);
            }
        }
       

        private async void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                string search = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(search))
                {
                    LoadData();
                    return;
                }

                var result = await subjectController.SearchSubjectsByNameAsync(search);
                dataGridView1.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
            }


        }

        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int subjectId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SubjectID"].Value);

                    var subject = new Subject
                    {
                        SubjectID = subjectId,
                        SubjectName = txtSubject.Text.Trim(),
                        CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
                    };

                    bool result = await subjectController.UpdateSubjectAsync(subject);
                    MessageBox.Show(result ? "Subject updated." : "Update failed.");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }
        

        private async void btnDelete_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int subjectId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SubjectID"].Value);
                    bool result = await subjectController.DeleteSubjectAsync(subjectId);
                    MessageBox.Show(result ? "Deleted successfully." : "Delete failed.");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                var subject = new Subject
                {
                    SubjectName = txtSubject.Text.Trim(),
                    CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
                };

                bool result = await subjectController.AddSubjectAsync(subject);
                MessageBox.Show(result ? "Subject added!" : "Failed to add.");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
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
