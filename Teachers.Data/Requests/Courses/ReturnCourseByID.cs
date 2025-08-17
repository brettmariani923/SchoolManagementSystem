using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teachers.Data.Requests.Courses
{
    public class ReturnCourseByID
    {
        public string GetSql()
        {
            return "SELECT Course FROM SchoolSystem WHERE CourseID = @CourseID;";
        }

        public object? GetParameters()
        {
            return null;
        }
    }
}
