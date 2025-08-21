using Teachers.Data.Requests.Enrollments.Update;
using Teachers.Data.DTO;


namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class UpdateTests
    {
        [Fact]
        public void UpdateStudentEnrollment_GetSql_IsCorrect()
        {
            var req = new UpdateStudentEnrollment(1, 10, 20, 30, 40);
            Assert.Equal(
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
        }

        [Fact]
        public void UpdateStudentEnrollment_GetParameters_ProjectsFields()
        {
            var req = new UpdateStudentEnrollment(1, 10, 20, 30, 40);
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
            var req = new UpdateBulkEnrollments(Array.Empty<Enrollments_Row>());
            Assert.Equal(
                "UPDATE dbo.Enrollments " +
                "SET StudentID = @StudentID, " +
                "TeacherID = @TeacherID, " +
                "CourseID  = @CourseID, " +
                "SchoolID  = @SchoolID " +
                "WHERE EnrollmentID = @EnrollmentID;",
                req.GetSql());
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
