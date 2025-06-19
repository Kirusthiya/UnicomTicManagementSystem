namespace UnicomTicManagementSystem.View
{
    partial class LectureMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTimetable = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.dgvLectureMenu = new System.Windows.Forms.DataGridView();
            this.btnview = new System.Windows.Forms.Button();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLectureMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTimetable);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnMark);
            this.panel1.Controls.Add(this.dgvLectureMenu);
            this.panel1.Controls.Add(this.btnview);
            this.panel1.Controls.Add(this.txtUserId);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 596);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnTimetable
            // 
            this.btnTimetable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnTimetable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimetable.ForeColor = System.Drawing.Color.White;
            this.btnTimetable.Location = new System.Drawing.Point(344, 287);
            this.btnTimetable.Name = "btnTimetable";
            this.btnTimetable.Size = new System.Drawing.Size(106, 38);
            this.btnTimetable.TabIndex = 7;
            this.btnTimetable.Text = "Timetable";
            this.btnTimetable.UseVisualStyleBackColor = false;
            this.btnTimetable.Click += new System.EventHandler(this.btnTimetable_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(466, 81);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMark
            // 
            this.btnMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMark.ForeColor = System.Drawing.Color.White;
            this.btnMark.Location = new System.Drawing.Point(191, 287);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(106, 38);
            this.btnMark.TabIndex = 5;
            this.btnMark.Text = "Add mark";
            this.btnMark.UseVisualStyleBackColor = false;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // dgvLectureMenu
            // 
            this.dgvLectureMenu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvLectureMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLectureMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvLectureMenu.Location = new System.Drawing.Point(0, 401);
            this.dgvLectureMenu.Name = "dgvLectureMenu";
            this.dgvLectureMenu.Size = new System.Drawing.Size(665, 195);
            this.dgvLectureMenu.TabIndex = 4;
            this.dgvLectureMenu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLectureMenu_CellContentClick);
            // 
            // btnview
            // 
            this.btnview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnview.ForeColor = System.Drawing.Color.White;
            this.btnview.Location = new System.Drawing.Point(39, 287);
            this.btnview.Name = "btnview";
            this.btnview.Size = new System.Drawing.Size(106, 38);
            this.btnview.TabIndex = 3;
            this.btnview.Text = "View details";
            this.btnview.UseVisualStyleBackColor = false;
            this.btnview.Click += new System.EventHandler(this.btnview_Click);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(142, 120);
            this.txtUserId.Multiline = true;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(155, 31);
            this.txtUserId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "User ID";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lecturer View";
            // 
            // LectureMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 596);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LectureMenuForm";
            this.Text = "LectureMenuForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLectureMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvLectureMenu;
        private System.Windows.Forms.Button btnview;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnTimetable;
        private System.Windows.Forms.Button btnLogout;
    }
}