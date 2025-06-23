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
    public partial class LecturerVireForm : Form
    {
        private readonly string lecturerUserID;
        private readonly Lecturecontroller lectureController = new Lecturecontroller();
        private readonly MarkController markController = new MarkController();
        private readonly TimeTableController timetableController = new TimeTableController();
        public LecturerVireForm(string userId)
        {
            InitializeComponent();
            lecturerUserID = userId;
            txtUserId.Text = lecturerUserID;
            txtUserId.ReadOnly = true;
        }

        public void LoadForm(Form form)
        {
            try
            {
                foreach (Control ctrl in LecturePanel.Controls)
                {
                    ctrl.Hide();
                    ctrl.Dispose();
                }
                LecturePanel.Controls.Clear();

                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                LecturePanel.Controls.Add(form);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form: " + ex.Message);
            }
        }
        private async void btnview_Click(object sender, EventArgs e)
        {
            var allLecturers = await lectureController.GetAllLecturersAsync();
            var lecturer = allLecturers.FirstOrDefault(l => l.UserID == lecturerUserID);

            if (lecturer != null)
            {
                dataGridView1.DataSource = new List<Lecture> { lecturer };

                if (!dataGridView1.Columns.Contains("ViewDetails"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                    {
                        Name = "ViewDetails",
                        HeaderText = "Action",
                        Text = "View",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridView1.Columns.Add(btn);
                }
            }
            else
            {
                MessageBox.Show("Lecturer not found.");
                dataGridView1.Rows.Clear();
            }
        }
        private void btnMark_Click(object sender, EventArgs e)
        { 
            LoadForm(new MarkForm());
        }
        

        
        private async void btnTimetable_Click(object sender, EventArgs e)
        {
            var timetableList = await timetableController.GetAllTimetablesAsync();
            var lecturerTimetable = timetableList.Where(t => t.UserID == lecturerUserID).ToList();

            if (lecturerTimetable.Any())
            {
                dataGridView1.DataSource = lecturerTimetable;
            }
            else
            {
                MessageBox.Show("No timetable found for this lecturer.");
                dataGridView1.Rows.Clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                var userIdValue = selectedRow.Cells["UserID"].Value?.ToString();

                var clickedColumn = dataGridView1.Columns[e.ColumnIndex];

                if (clickedColumn?.Name == "ViewDetails" && !string.IsNullOrEmpty(userIdValue))
                {
                    MessageBox.Show("Show details for UserID: " + userIdValue);
                }
            }
        }
        //Logout=================
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}
