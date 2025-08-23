using Teachers.Data.Rows;
using Teachers.Data.Requests.Enrollments.Update;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class UpdateTests
    {
        private const string ExpectedSql =
            "UPDATE dbo.Enrollments " +
            "SET StudentID = @StudentID, " +
            "TeacherID = @TeacherID, " +
            "CourseID  = @CourseID, " +
            "SchoolID  = @SchoolID " +
            "WHERE EnrollmentID = @EnrollmentID;";

        [Fact]
        public void UpdateStudentEnrollment_GetSql_IsCorrect()
        {
            var row = new Enrollments_Row
            {
                EnrollmentID = 1,
                TeacherID = 20,
                CourseID = 30,
                SchoolID = 40
            };

            var req = new UpdateStudentEnrollment(row);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateStudentEnrollment_GetParameters_ProjectsFields()
        {
            var row = new Enrollments_Row
            {
                EnrollmentID = 1,
                StudentID = 10,
                TeacherID = 20,
                CourseID = 30,
                SchoolID = 40
            };

            var req = new UpdateStudentEnrollment(row);

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(1, (int)t.GetProperty("EnrollmentID")!.GetValue(p)!);
            Assert.Equal(10, (int)t.GetProperty("StudentID")!.GetValue(p)!);
            Assert.Equal(20, (int)t.GetProperty("TeacherID")!.GetValue(p)!);
            Assert.Equal(30, (int)t.GetProperty("CourseID")!.GetValue(p)!);
            Assert.Equal(40, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void UpdateBulkEnrollments_GetSql_IsCorrect()
        {
            // Use at least one item to satisfy typical non-empty validation in ctor
            var items = new[]
            {
                new Enrollments_Row { EnrollmentID = 1, StudentID = 10, TeacherID = 20, CourseID = 30, SchoolID = 40 }
            };

            var req = new UpdateBulkEnrollments(items);

            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void UpdateBulkEnrollments_GetParameters_CountMatchesInput()
        {
            var items = new[]
            {
                new Enrollments_Row { EnrollmentID = 1, StudentID = 10, TeacherID = 20, CourseID = 30, SchoolID = 40 },
                new Enrollments_Row { EnrollmentID = 2, StudentID = 11, TeacherID = 21, CourseID = 31, SchoolID = 41 },
            };

            var req = new UpdateBulkEnrollments(items);

            var list = ((IEnumerable<object>)req.GetParameters()!).ToList();
            Assert.Equal(items.Length, list.Count);
        }
    }
}
