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
    public partial class StudenSubjectForm : Form
    {
        private readonly StudentSubjectConteroller _controller = new StudentSubjectConteroller();
        private int selectedId = -1;

        public StudenSubjectForm()
        {
            InitializeComponent();
        }
        private async void StudentSubjectsForm_Load(object sender, EventArgs e)
        {
            await LoadStudentSubjectsAsync();
        }

        private async Task LoadStudentSubjectsAsync()
        {
            try
            {
                var list = await _controller.GetAllStudentSubjectsAsync();
                dgvStudentSubject.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudent.Text) || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("UserID and SubjectID are required.");
                return;
            }

            if (!int.TryParse(txtSubject.Text, out int subjectId))
            {
                MessageBox.Show("Subject ID must be a valid number.");
                return;
            }

            try
            {
                bool result = await _controller.AddStudentSubjectAsync(txtStudent.Text, subjectId);
                if (result)
                {
                    MessageBox.Show("Student subject added successfully.");
                    ClearFields();
                    await LoadStudentSubjectsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to add student subject.");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Error adding student subject");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this record?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool result = await _controller.DeleteStudentSubjectAsync(selectedId);
                    if (result)
                    {
                        MessageBox.Show("Deleted successfully.");
                        ClearFields();
                        await LoadStudentSubjectsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete.");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("Error deleting" );
                }
            }
        }

        private async void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string name = txtSearch.Text.Trim();
                var result = await _controller.SearchByStudentNameAsync(name);
                dgvStudentSubject.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching: " + ex.Message);
            }
        }

        private void dgvStudentSubject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvStudentSubject.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["Id"].Value);
                txtStudent.Text = row.Cells["UserID"].Value.ToString();
                txtSubject.Text = row.Cells["SubjectID"].Value.ToString();
            }
        }
        private void ClearFields()
        {
            txtStudent.Clear();
            txtSubject.Clear();
            selectedId = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ClearFields();
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
