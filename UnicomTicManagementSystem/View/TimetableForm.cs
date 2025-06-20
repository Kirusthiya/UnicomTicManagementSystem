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
            _ = LoadData();
        }


        private async Task LoadData()
        {
            try
            {
                dgvTimetable.AutoGenerateColumns = true; 
                timetableList = await controller.GetAllTimetablesAsync();
                dgvTimetable.DataSource = timetableList;
                dgvTimetable.Refresh();

              
                var subjects = await new SubjectController().GetAllSubjectsAsync();
                cmbSubject.DataSource = subjects;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubjectID";

               
                var rooms = await new RoomContoller().GetAllRoomsAsync();
                cmbRoom.DataSource = rooms;
                cmbRoom.DisplayMember = "RoomName";
                cmbRoom.ValueMember = "RoomID";

               
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
                    await LoadData();
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
                    await LoadData();
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTimetable.Rows[e.RowIndex];
                cmbSubject.SelectedValue = row.Cells["SubjectID"].Value;
                cmbRoom.SelectedValue = row.Cells["RoomID"].Value;
                txtTime.Text = row.Cells["TimeSlot"].Value.ToString();
                cmbDay.Text = row.Cells["Day"].Value.ToString();
                cmbLEcture.SelectedValue = row.Cells["UserID"].Value;
            }
        }

        private async void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                Timetable newTimetable = new Timetable
                {
                    SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                    RoomID = Convert.ToInt32(cmbRoom.SelectedValue),
                    TimeSlot = txtTime.Text.Trim(),
                    Day = cmbDay.Text,
                    UserID = cmbLEcture.SelectedValue.ToString()
                };

                bool success = await controller.AddTimetableAsync(newTimetable);
                if (success)
                {
                     MessageBox.Show("Timetable added successfully!");
                    await LoadData();  
                }
                else
                {
                    MessageBox.Show("Failed to add timetable.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding timetable: " + ex.Message);
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
