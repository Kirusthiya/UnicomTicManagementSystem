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
    public partial class RoomForm : Form
    {
        private readonly RoomContoller _controller = new RoomContoller();
        private int selectedRoomId = -1;
        public RoomForm()
        {
            InitializeComponent();
            LoadRooms();
        }

        private async void LoadRooms()
        {
            try
            {
                var rooms = await _controller.GetAllRoomsAsync();
                dgvRoom.DataSource = rooms;
                dgvRoom.ClearSelection();
            }
            catch (Exception )
            {
                MessageBox.Show("Error loading rooms " );
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomname.Text) || string.IsNullOrWhiteSpace(txtRoomtype.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var room = new Room
            {
                RoomName = txtRoomname.Text.Trim(),
                RoomType = txtRoomtype.Text.Trim()
            };

            if (await _controller.AddRoomAsync(room))
            {
                MessageBox.Show("Room added successfully.");
                ClearInputs();
                LoadRooms();
            }
            else
            {
                MessageBox.Show("Failed to add room.");
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRoomname.Text) || string.IsNullOrWhiteSpace(txtRoomtype.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            var room = new Room
            {
                RoomID = selectedRoomId,
                RoomName = txtRoomname.Text.Trim(),
                RoomType = txtRoomtype.Text.Trim()
            };

            if (await _controller.UpdateRoomAsync(room))
            {
                MessageBox.Show("Room updated successfully.");
                ClearInputs();
                LoadRooms();
            }
            else
            {
                MessageBox.Show("Failed to update room.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this room?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                if (await _controller.DeleteRoomAsync(selectedRoomId))
                {
                    MessageBox.Show("Room deleted successfully.");
                    ClearInputs();
                    LoadRooms();
                }
                else
                {
                    MessageBox.Show("Failed to delete room.");
                }
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                var search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                {
                    var results = await _controller.SearchRoomsAsync(search);
                    dgvRoom.DataSource = results;
                }
                else
                {
                    LoadRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during search: " + ex.Message);
            }
        }

        private void dgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dgvRoom.Rows[e.RowIndex].Cells["RoomID"].Value != null)
            {
                selectedRoomId = Convert.ToInt32(dgvRoom.Rows[e.RowIndex].Cells["RoomID"].Value);
                dgvRoom.Text = dgvRoom.Rows[e.RowIndex].Cells["RoomName"].Value.ToString();
                dgvRoom.Text = dgvRoom.Rows[e.RowIndex].Cells["RoomType"].Value.ToString();
            }
        }


        private void ClearInputs()
        {
            txtRoomname.Clear();
            txtRoomtype.Clear();
            selectedRoomId = -1;
            dgvRoom.ClearSelection();
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
