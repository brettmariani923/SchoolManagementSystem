using Teachers.Data.Rows;
using Teachers.Data.Requests.Enrollments.Insert;

namespace Teachers.Test.DataRequestTests.Enrollments
{
    public class InsertTests
    {
        private const string ExpectedSql =
            "INSERT INTO dbo.Enrollments (StudentID, TeacherID, CourseID, SchoolID)" +
             "VALUES (@StudentID, @TeacherID, @CourseID, @SchoolID);";

        [Fact]
        public void InsertEnrollment_GetSql()
        {
            var row = new Enrollments_Row
            {
                StudentID = 101,
                TeacherID = 7,
                CourseID = 55,
                SchoolID = 3
            };

            var req = new InsertStudentEnrollment(row);
            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void InsertEnrollment_GetParameters()
        {
            var row = new Enrollments_Row
            {
                StudentID = 101,
                TeacherID = 7,
                CourseID = 55,
                SchoolID = 3
            };

            var req = new InsertStudentEnrollment(row);

            var p = req.GetParameters()!;
            var t = p.GetType();

            Assert.Equal(101, (int)t.GetProperty("StudentID")!.GetValue(p)!);
            Assert.Equal(7, (int)t.GetProperty("TeacherID")!.GetValue(p)!);
            Assert.Equal(55, (int)t.GetProperty("CourseID")!.GetValue(p)!);
            Assert.Equal(3, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
        }

        [Fact]
        public void InsertBulkEnrollments_GetSql()
        {
            var rows = new[]
            {
                new Enrollments_Row { StudentID = 101, TeacherID = 7, CourseID = 55, SchoolID = 3 },
                new Enrollments_Row { StudentID = 102, TeacherID = 7, CourseID = 55, SchoolID = 3 }
            };

            var req = new InsertBulkStudentEnrollment(rows);
            Assert.Equal(ExpectedSql, req.GetSql());
        }

        [Fact]
        public void InsertBulkEnrollments_GetParameters()
        {
            var rows = new[]
            {
                new Enrollments_Row { StudentID = 101, TeacherID = 7, CourseID = 55, SchoolID = 3 },
                new Enrollments_Row { StudentID = 102, TeacherID = 7, CourseID = 55, SchoolID = 3 }
            };

            var req = new InsertBulkStudentEnrollment(rows);

            var list = ((IEnumerable<object>)req.GetParameters()!).ToList();
            Assert.Equal(2, list.Count);

            var first = list[0];
            var t = first.GetType();

            Assert.Equal(101, (int)t.GetProperty("StudentID")!.GetValue(first)!);
            Assert.Equal(7, (int)t.GetProperty("TeacherID")!.GetValue(first)!);
            Assert.Equal(55, (int)t.GetProperty("CourseID")!.GetValue(first)!);
            Assert.Equal(3, (int)t.GetProperty("SchoolID")!.GetValue(first)!);
        }
    }
}
