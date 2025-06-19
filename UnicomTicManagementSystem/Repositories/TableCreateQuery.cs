using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagementSystem.Repositories
{
    public static class TableCreateQuery
    {
        public static void CreateTables()
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string CreateTables = @"
                CREATE TABLE IF NOT EXISTS Users ( 
                       UserID TEXT PRIMARY KEY, 
                       Username TEXT NOT NULL UNIQUE,
                       Password TEXT NOT NULL, 
                       Role TEXT NOT NULL 
                );

                 CREATE TABLE IF NOT EXISTS Courses (
                      CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                      CourseName TEXT NOT NULL
                );

                 CREATE TABLE IF NOT EXISTS Subjects (
                    SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                    SubjectName TEXT NOT NULL,
                    CourseID INTEGER,
                    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
                );

                 CREATE TABLE IF NOT EXISTS Students (
                     UserID TEXT PRIMARY KEY,
                     Name TEXT NOT NULL,
                     Gender TEXT NOT NULL,
                     Address TEXT NOT NULL,
                     PhoneNumber TEXT NOT NULL,
                     CourseID INTEGER,
                     FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
                     FOREIGN KEY (UserID) REFERENCES Users(UserID)
                );

                    CREATE TABLE IF NOT EXISTS Staff (
                        UserID TEXT PRIMARY KEY,
                        Name TEXT NOT NULL,
                        Address TEXT,
                        Gender TEXT,
                        Position TEXT NOT NULL,
                        Salary REAL,
                        PhoneNumber TEXT,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID)
                    );
                    CREATE TABLE IF NOT EXISTS Lecturers (
                        UserID TEXT PRIMARY KEY,
                        Name TEXT NOT NULL,
                        Address TEXT,
                        Gender TEXT,
                        Salary REAL,
                        PhoneNumber TEXT,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID)
                    );

                    CREATE TABLE IF NOT EXISTS Rooms (
                        RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoomName TEXT NOT NULL,
                        RoomType TEXT NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS Timetables (
                        TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectID INTEGER NOT NULL,
                        UserID TEXT NOT NULL,
                        TimeSlot TEXT NOT NULL,
                        RoomID INTEGER NOT NULL,
                        Day TEXT NOT NULL, 
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
                    );

                    CREATE TABLE IF NOT EXISTS Exams (
                        ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExamName TEXT NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        CourseID INTEGER NOT NULL,
                        FileName TEXT,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
                    );

                    CREATE TABLE IF NOT EXISTS Marks (
                        MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID TEXT NOT NULL,
                        ExamID INTEGER NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        Score INTEGER NOT NULL,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );

                    CREATE TABLE IF NOT EXISTS StudentSubjects (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID TEXT NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );

                    CREATE TABLE IF NOT EXISTS LecturerSubjects (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserID TEXT NOT NULL,
                        SubjectID INTEGER NOT NULL,
                        FOREIGN KEY (UserID) REFERENCES Users(UserID),
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    );
                    ";

                SQLiteCommand cmd = new SQLiteCommand(CreateTables, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
     