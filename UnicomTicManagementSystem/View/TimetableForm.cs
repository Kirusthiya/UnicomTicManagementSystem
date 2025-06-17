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
    public partial class TimetableForm : Form
    {
        private readonly TimeTableController controller = new TimeTableController();
        private List<Timetable> timetableList = new List<Timetable>();

        public TimetableForm()
        {
            InitializeComponent();
            LoadData();
        }


        private async void LoadData()
        {
            try
            {
                // Load Timetables
                timetableList = await controller.GetAllTimetablesAsync();
                dgvTimetable.DataSource = timetableList;

                // Load Subjects
                var subjects = await new SubjectController().GetAllSubjectsAsync();
                cmbSubject.DataSource = subjects;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubjectID";

                // Load Rooms
                var rooms = await new RoomContoller().GetAllRoomsAsync();
                cmbRoom.DataSource = rooms;
                cmbRoom.DisplayMember = "RoomName";
                cmbRoom.ValueMember = "RoomID";

                // Load Lecturers (Users with Role = 'Lecturer')
                var users = await new UserController().GetAllUsersAsync();
                var lecturers = users.Where(u => u.Role == "Lecturer").ToList();
                cmbLEcture.DataSource = lecturers;
                cmbLEcture.DisplayMember = "Name";
                cmbLEcture.ValueMember = "UserID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {
                try
                {
                    if (dgvTimetable.CurrentRow != null)
                    {
                        int id = Convert.ToInt32(dgvTimetable.CurrentRow.Cells["TimetableID"].Value);

                        var updatedTimetable = new Timetable
                        {
                            TimetableID = id,
                            SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                            RoomID = Convert.ToInt32(cmbRoom.SelectedValue),
                            TimeSlot = txtTime.Text.Trim(),
                            Day = cmbDay.Text,
                            UserID = Convert.ToString(cmbLEcture.SelectedValue)
                        };

                       
                        var existingTimetables = await controller.GetAllTimetablesAsync();

                        bool conflict = existingTimetables.Any(t =>
                            t.TimetableID != id && 
                            t.Day == updatedTimetable.Day &&
                            t.TimeSlot == updatedTimetable.TimeSlot &&
                            (t.UserID == updatedTimetable.UserID || t.RoomID == updatedTimetable.RoomID)
                        );

                        if (conflict)
                        {
                            MessageBox.Show("Conflict Detected:\nLecturer or Room already assigned at this time.");
                            return;
                        }

                       
                        bool result = await controller.UpdateTimetableAsync(updatedTimetable);
                        MessageBox.Show(result ? "Timetable updated!" : "Update failed.");
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
                if (dgvTimetable.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgvTimetable.CurrentRow.Cells["TimetableID"].Value);
                    bool result = await controller.DeleteTimetableAsync(id);
                    MessageBox.Show(result ? "Deleted successfully." : "Delete failed.");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbLEcture_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvTimetable_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvTimetable.Rows.Count)
                {
                    var row = dgvTimetable.Rows[e.RowIndex];
                    cmbSubject.SelectedValue = row.Cells["SubjectID"].Value;
                    cmbRoom.SelectedValue = row.Cells["RoomID"].Value;
                    txtTime.Text = row.Cells["TimeSlot"].Value.ToString();
                    cmbDay.Text = row.Cells["Day"].Value.ToString();
                    cmbLEcture.SelectedValue = row.Cells["UserID"].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selection Error: " + ex.Message);
            }
        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
                try
                {
                    var newTimetable = new Timetable
                    {
                        SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                        RoomID = Convert.ToInt32(cmbRoom.SelectedValue),
                        TimeSlot = txtTime.Text.Trim(),
                        Day = cmbDay.Text,
                        UserID = Convert.ToString(cmbLEcture.SelectedValue)
                    };

                    // Validation Check
                    var existingTimetables = await controller.GetAllTimetablesAsync();

                    bool conflict = existingTimetables.Any(t =>
                        t.Day == newTimetable.Day &&
                        t.TimeSlot == newTimetable.TimeSlot &&
                        (t.UserID == newTimetable.UserID || t.RoomID == newTimetable.RoomID)
                    );

                    if (conflict)
                    {
                        MessageBox.Show("Conflict Detected:\nLecturer or Room already assigned at this time.");
                        return;
                    }

                    // If no conflict, proceed to add
                    bool result = await controller.AddTimetableAsync(newTimetable);
                    MessageBox.Show(result ? "Timetable added!" : "Failed to add.");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Add Error: " + ex.Message);
                }
            
        }

        private void txtTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
