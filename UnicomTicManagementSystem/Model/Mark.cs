using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagementSystem.Model
{
    public class Mark
    {
        public int MarkID { get; set; }
        public string UserID { get; set; }
        public string StudentName { get; set; }
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int Score {  get; set; }
    }
}
