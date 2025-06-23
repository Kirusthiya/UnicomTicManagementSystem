using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagementSystem.Controller;

namespace UnicomTicManagementSystem.View
{

    public partial class StudentViewForm : Form
    {
        private readonly StudentController studentController = new StudentController();
        private readonly TimeTableController timetableController = new TimeTableController();
        private readonly MarkController markController = new MarkController();
        public StudentViewForm()
        {
            InitializeComponent();
        }

      

        private async void btnDetails_Click(object sender, EventArgs e)
        {
            string userId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please enter User ID.");
                return;
            }
            var student = await studentController.GetStudentByUserIdAsync(userId);
            if (student != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("UserID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Gender");
                dt.Columns.Add("Address");
                dt.Columns.Add("PhoneNumber");
                dt.Columns.Add("CourseID");

                dt.Rows.Add(student.UserID, student.Name, student.Gender, student.Address, student.PhoneNumber, student.CourseID);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        private async void btnTimetable_Click(object sender, EventArgs e)
        {
            string userId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Enter User ID first.");
                return;
            }

            var timetableList = await timetableController.GetTimetablesByUserIDAsync(userId);
            dataGridView1.DataSource = timetableList;
        }

        private async void btnExamScore_Click(object sender, EventArgs e)
        {
            string userId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Enter User ID first.");
                return;
            }

            var markList = await markController.GetAllMarksAsync();
            dataGridView1.DataSource = markList;
        }

       
        public void LoadForm(Form form)
        {

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

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        

        private void textBox1_Enter(object sender, EventArgs e)
        {
        }
        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            

        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var value = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                if (value != null)
                    MessageBox.Show(value.ToString(), "Cell Content");
            }
        }
    }
}
    

