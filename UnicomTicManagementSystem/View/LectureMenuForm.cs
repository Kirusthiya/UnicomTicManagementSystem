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

            var timetableController = new TimeTableController();
            var timetableList = await timetableController.GetAllTimetablesAsync();

            if (timetableList.Any())
            {
                dgvLectureMenu.DataSource = timetableList;
            }
            else
            {
                MessageBox.Show("No timetable found.");
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

            var controller = new Lecturecontroller();
            var allLecturers = await controller.GetAllLecturersAsync();
            var lecturer = allLecturers.FirstOrDefault(l => l.UserID == userId);

            if (lecturer != null)
            {
                dgvLectureMenu.DataSource = new List<Lecture> { lecturer };
            }
            else
            {
                MessageBox.Show("Lecturer not found.");
                dgvLectureMenu.DataSource = null;
            }
        }
     

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
