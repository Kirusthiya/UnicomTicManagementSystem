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
    public partial class LectureMenuForm : Form
    {
        private readonly string lecturerUserID;
        private readonly Lecturecontroller lectureController = new Lecturecontroller();
        private readonly MarkController markController = new MarkController();
        private readonly TimeTableController timetableController = new TimeTableController();


        public LectureMenuForm(string userId)
        {
            InitializeComponent();
            lecturerUserID = userId;
            txtUserId.Text = "";
            
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btnview_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter your User ID.");
                return;
            }

            var allLecturers = await lectureController.GetAllLecturersAsync();
            var lecturer = allLecturers.FirstOrDefault(l => l.UserID == userId);

            if (lecturer != null)
            {
                dgvLectureMenu.DataSource = new List<Lecture> { lecturer };

                // Check if "ViewDetails" column exists
                if (!dgvLectureMenu.Columns.Contains("ViewDetails"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "ViewDetails";
                    btn.HeaderText = "Action";
                    btn.Text = "View";
                    btn.UseColumnTextForButtonValue = true;
                    dgvLectureMenu.Columns.Add(btn);
                }
            }
            else
            {
                MessageBox.Show("Lecturer not found.");
                dgvLectureMenu.DataSource = null;
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
         
            string userId = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter your User ID.");
                return;
            }
            else
            {

                LoadForm(new Form1());

            }
            //// Assuming you have a form to handle marks
            //Mark markForm = new Mark();
            //markForm.ShowDialog();
        }
       
        

        private async void btnTimetable_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter your User ID.");
                return;
            }

            var timetableList = await timetableController.GetAllTimetablesAsync();
            var lecturerTimetable = timetableList.Where(t => t.UserID == userId).ToList();

            if (lecturerTimetable.Any())
            {
                dgvLectureMenu.DataSource = lecturerTimetable;
            }
            else
            {
                MessageBox.Show("No timetable found for this lecturer.");
                dgvLectureMenu.DataSource = null;
            }
        }
        
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LoadForm(new LoginForm("Lecturer"));

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvLectureMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Row and Column click check
            if (e.RowIndex >= 0)
            {
                // Row edukkuthu
                DataGridViewRow selectedRow = dgvLectureMenu.Rows[e.RowIndex];

                // Cell la 'UserID' column irundha value edukkuthu
                object userIdValue = selectedRow.Cells["UserID"].Value;
                string userId = "";

                if (userIdValue != null)
                {
                    userId = userIdValue.ToString();
                }

                // Column name check
                DataGridViewColumn clickedColumn = dgvLectureMenu.Columns[e.ColumnIndex];

                if (clickedColumn != null)
                {
                    if (clickedColumn.Name == "ViewDetails")
                    {
                        MessageBox.Show("Show details for UserID: " + userId);
                    }
                }
            }
        }

    }
    
}
