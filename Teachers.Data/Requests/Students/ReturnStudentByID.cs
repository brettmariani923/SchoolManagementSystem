using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teachers.Data.Requests.Students
{
    public class ReturnStudentByID
    {
        private readonly int _studentID;
        private readonly int _firstName;
        private readonly int _lastName;

        public ReturnStudentByID(int studentID, int firstName, int lastName)
        {
            studentID = _studentID;
            firstName = _firstName;
            lastName = _lastName;

        }
        public string GetSql()
        {
            return @"Select Student FROM SchoolSystem WHERE StudentID = @StudentID;";
        }

        public object? GetParameters()
        {
            return new { StudentID = _studentID };
    }
}
