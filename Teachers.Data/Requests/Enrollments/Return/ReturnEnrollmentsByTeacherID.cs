using System;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public sealed class ReturnEnrollmentsByTeacherID : IDataFetchList<Enrollments_Row>
    {
        private readonly int _teacherID;

        public ReturnEnrollmentsByTeacherID(int teacherID)
        {
            if (teacherID <= 0)
                throw new ArgumentOutOfRangeException(nameof(teacherID), "TeacherID must be positive.");

            _teacherID = teacherID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
              "FROM dbo.Enrollments" +
              "WHERE TeacherID = @TeacherID;";

        public object GetParameters() => new { TeacherID = _teacherID };
    }
}
