using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teachers.Application.DTO
{
    public class TeacherRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SchoolID { get; set; }
    }
}
