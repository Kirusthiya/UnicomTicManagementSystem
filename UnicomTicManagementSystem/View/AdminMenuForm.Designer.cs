namespace UnicomTicManagementSystem.View
{
    partial class AdminMenuForm
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
            this.AdminPanel = new System.Windows.Forms.Panel();
            this.btnstudenrSubject = new System.Windows.Forms.Button();
            this.LectureSubject = new System.Windows.Forms.Button();
            this.btnRoom = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.btnTimetable = new System.Windows.Forms.Button();
            this.btnExam = new System.Windows.Forms.Button();
            this.btnCourse = new System.Windows.Forms.Button();
            this.btnSubject = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.BtnAddLecture = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.AdminPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdminPanel
            // 
            this.AdminPanel.Controls.Add(this.btnstudenrSubject);
            this.AdminPanel.Controls.Add(this.LectureSubject);
            this.AdminPanel.Controls.Add(this.btnRoom);
            this.AdminPanel.Controls.Add(this.btnMark);
            this.AdminPanel.Controls.Add(this.btnTimetable);
            this.AdminPanel.Controls.Add(this.btnExam);
            this.AdminPanel.Controls.Add(this.btnCourse);
            this.AdminPanel.Controls.Add(this.btnSubject);
            this.AdminPanel.Controls.Add(this.btnAdd);
            this.AdminPanel.Controls.Add(this.btnAddStudent);
            this.AdminPanel.Controls.Add(this.BtnAddLecture);
            this.AdminPanel.Controls.Add(this.btnAddUser);
            this.AdminPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AdminPanel.Location = new System.Drawing.Point(0, 0);
            this.AdminPanel.Name = "AdminPanel";
            this.AdminPanel.Size = new System.Drawing.Size(665, 596);
            this.AdminPanel.TabIndex = 0;
            // 
            // btnstudenrSubject
            // 
            this.btnstudenrSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstudenrSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnstudenrSubject.Location = new System.Drawing.Point(257, 401);
            this.btnstudenrSubject.Name = "btnstudenrSubject";
            this.btnstudenrSubject.Size = new System.Drawing.Size(135, 60);
            this.btnstudenrSubject.TabIndex = 13;
            this.btnstudenrSubject.Text = "Student Subject Details";
            this.btnstudenrSubject.UseVisualStyleBackColor = true;
            this.btnstudenrSubject.Click += new System.EventHandler(this.btnstudenrSubject_Click);
            // 
            // LectureSubject
            // 
            this.LectureSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LectureSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.LectureSubject.Location = new System.Drawing.Point(440, 401);
            this.LectureSubject.Name = "LectureSubject";
            this.LectureSubject.Size = new System.Drawing.Size(135, 60);
            this.LectureSubject.TabIndex = 12;
            this.LectureSubject.Text = "Lecture assigen Subject";
            this.LectureSubject.UseVisualStyleBackColor = true;
            this.LectureSubject.Click += new System.EventHandler(this.LectureSubject_Click);
            // 
            // btnRoom
            // 
            this.btnRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnRoom.Location = new System.Drawing.Point(70, 401);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Size = new System.Drawing.Size(135, 60);
            this.btnRoom.TabIndex = 11;
            this.btnRoom.Text = "Room";
            this.btnRoom.UseVisualStyleBackColor = true;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // btnMark
            // 
            this.btnMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnMark.Location = new System.Drawing.Point(257, 310);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(135, 60);
            this.btnMark.TabIndex = 9;
            this.btnMark.Text = "Mark";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnTimetable
            // 
            this.btnTimetable.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimetable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnTimetable.Location = new System.Drawing.Point(440, 310);
            this.btnTimetable.Name = "btnTimetable";
            this.btnTimetable.Size = new System.Drawing.Size(135, 60);
            this.btnTimetable.TabIndex = 10;
            this.btnTimetable.Text = "Timetable";
            this.btnTimetable.UseVisualStyleBackColor = true;
            this.btnTimetable.Click += new System.EventHandler(this.btnTimetable_Click);
            // 
            // btnExam
            // 
            this.btnExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnExam.Location = new System.Drawing.Point(70, 310);
            this.btnExam.Name = "btnExam";
            this.btnExam.Size = new System.Drawing.Size(135, 60);
            this.btnExam.TabIndex = 8;
            this.btnExam.Text = "Exam";
            this.btnExam.UseVisualStyleBackColor = true;
            this.btnExam.Click += new System.EventHandler(this.btnExam_Click);
            // 
            // btnCourse
            // 
            this.btnCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCourse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCourse.Location = new System.Drawing.Point(257, 226);
            this.btnCourse.Name = "btnCourse";
            this.btnCourse.Size = new System.Drawing.Size(135, 60);
            this.btnCourse.TabIndex = 7;
            this.btnCourse.Text = "Course";
            this.btnCourse.UseVisualStyleBackColor = true;
            this.btnCourse.Click += new System.EventHandler(this.btnCourse_Click);
            // 
            // btnSubject
            // 
            this.btnSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSubject.Location = new System.Drawing.Point(440, 226);
            this.btnSubject.Name = "btnSubject";
            this.btnSubject.Size = new System.Drawing.Size(135, 60);
            this.btnSubject.TabIndex = 6;
            this.btnSubject.Text = "Subject";
            this.btnSubject.UseVisualStyleBackColor = true;
            this.btnSubject.Click += new System.EventHandler(this.btnSubject_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnAdd.Location = new System.Drawing.Point(70, 226);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 60);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add staff";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnAddStudent.Location = new System.Drawing.Point(257, 135);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(135, 60);
            this.btnAddStudent.TabIndex = 1;
            this.btnAddStudent.Text = "Add student";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            // 
            // BtnAddLecture
            // 
            this.BtnAddLecture.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddLecture.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BtnAddLecture.Location = new System.Drawing.Point(440, 135);
            this.BtnAddLecture.Name = "BtnAddLecture";
            this.BtnAddLecture.Size = new System.Drawing.Size(135, 60);
            this.BtnAddLecture.TabIndex = 1;
            this.BtnAddLecture.Text = "Add lecturer";
            this.BtnAddLecture.UseVisualStyleBackColor = true;
            this.BtnAddLecture.Click += new System.EventHandler(this.BtnAddLecture_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnAddUser.Location = new System.Drawing.Point(70, 135);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(135, 60);
            this.btnAddUser.TabIndex = 0;
            this.btnAddUser.Text = "Add user";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // AdminMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 596);
            this.Controls.Add(this.AdminPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminMenuForm";
            this.Text = "AdminMenuForm";
            this.AdminPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AdminPanel;
        private System.Windows.Forms.Button btnCourse;
        private System.Windows.Forms.Button btnSubject;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button BtnAddLecture;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnstudenrSubject;
        private System.Windows.Forms.Button LectureSubject;
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnTimetable;
        private System.Windows.Forms.Button btnExam;
    }
}