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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            LoadForm(new UserLoginCreate());
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            LoadForm(new StudentManege());
        }

        private void BtnAddLecture_Click(object sender, EventArgs e)
        {
          LoadForm(new LeactureManage());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadForm(new StaffManege());
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            LoadForm(new CourseForm());
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            LoadForm(new SubjectForm());
        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            LoadForm(new ExamForm());
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            LoadForm(new Form1());
        }

        private void btnTimetable_Click(object sender, EventArgs e)
        {
            LoadForm(new TimetableForm());
        }

        private void btnstudenrSubject_Click(object sender, EventArgs e)
        {
            LoadForm(new StudenSubjectForm());
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            LoadForm(new RoomForm());
        }

        private void LectureSubject_Click(object sender, EventArgs e)
        {
            LoadForm(new LectureSubjectForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm("Admin");
            loginForm.TopLevel = false;
            loginForm.FormBorderStyle = FormBorderStyle.None;
            loginForm.Dock = DockStyle.Fill;

            this.Controls.Clear();
            this.Controls.Add(loginForm);
            loginForm.Show();
           
        }
    }
}
