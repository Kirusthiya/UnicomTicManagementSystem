using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagementSystem.Model
{
    public class LecturerSubject
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string LecturerName { get; set; }
    }
}
