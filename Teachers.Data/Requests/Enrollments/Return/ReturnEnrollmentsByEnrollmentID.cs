using System;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Enrollments.Return
{
    public sealed class ReturnEnrollmentsByEnrollmentID : IDataFetchList<Enrollments_Row>
    {
        private readonly int _enrollmentID;

        public ReturnEnrollmentsByEnrollmentID(int enrollmentID)
        {
            if (enrollmentID <= 0)
                throw new ArgumentOutOfRangeException(nameof(enrollmentID), "EnrollmentID must be positive.");

            _enrollmentID = enrollmentID;
        }

        public string GetSql() =>
            @"SELECT EnrollmentID, StudentID, TeacherID, CourseID, SchoolID" +
              "FROM dbo.Enrollments" +
              "WHERE EnrollmentID = @EnrollmentID;";

        public object GetParameters() => new { EnrollmentID = _enrollmentID };
    }
}
