using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicomTicManagementSystem.View
{
    public partial class AdminMenuForm : Form
    {
        public AdminMenuForm()
        {
            InitializeComponent();
            LoadForm(new MainDashboad());
        }
        
        public void LoadForm(Form form)
        {
            
            foreach (Control ctrl in adminpanel.Controls)
            {
                ctrl.Dispose();
            }
            adminpanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            adminpanel.Controls.Add(form);
            form.Show();

        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            LoadForm(new StudentManege());
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            LoadForm(new UserLoginCreate());
        }

        private void btnlecture_Click(object sender, EventArgs e)
        {
            LoadForm(new LeactureManage());
        }

        private void btnstaff_Click(object sender, EventArgs e)
        {
            LoadForm(new StaffManege());
        }

        private void btnRoomManage_Click(object sender, EventArgs e)
        {
            LoadForm(new RoomForm());
        }

        private void btnSubjectmanage_Click(object sender, EventArgs e)
        {
            LoadForm(new SubjectForm());
        }

        private void btnCourseMansge_Click(object sender, EventArgs e)
        {
            LoadForm(new CourseForm());
        }

        private void btnMarkManage_Click(object sender, EventArgs e)
        {
            LoadForm(new MarkForm());
        }

        private void btntimetableManege_Click(object sender, EventArgs e)
        {
            LoadForm(new TimetableForm());
        }

        private void btnExamManage_Click(object sender, EventArgs e)
        {
            LoadForm(new ExamForm());
        }

        private void btnLectureassigen_Click(object sender, EventArgs e)
        {
            LoadForm(new LectureSubjectForm());
        }

        private void btnStudentSubjectmanege_Click(object sender, EventArgs e)
        {
            LoadForm(new StudenSubjectForm());
        }
    }
}
