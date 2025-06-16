using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagementSystem.Model
{
    public class Staff:Person
    {
       
       public string Position { get; set; }
       public double Salary { get; set; }
        public string UserName { get; set; }
    }
}
