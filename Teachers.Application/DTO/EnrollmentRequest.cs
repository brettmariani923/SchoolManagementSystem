using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teachers.Application.DTO
{
    public class EnrollmentRequest
    {
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        public int CourseID { get; set; }
        public int SchoolID { get; set; }
    }
}
