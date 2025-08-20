using System;
using Teachers.Data.Requests.Students.Return;
using Xunit;

namespace Teachers.Test.DataRequestTests.Students
{
    public class ReturnTests
    {
        [Fact]
        public void ReturnAllStudents_GetSql_IsCorrect()
        {
            var req = new ReturnAllStudents();

            Assert.Equal(
            @"SELECT StudentID, FirstName, LastName, [Year], SchoolID" +
              "FROM dbo.Students;",
            req.GetSql());
        }

        [Fact]
        public void ReturnAllStudents_GetParameters_IsNull()
        {
            var req = new ReturnAllStudents();

            Assert.Null(req.GetParameters());
        }

        [Fact]
        public void ReturnStudentById_GetSql_IsCorrect()
        {
            var req = new ReturnStudentById(123);

            Assert.Equal(
               @"SELECT StudentID, FirstName, LastName, [Year], SchoolID" +
              "FROM dbo.Students" +
              "WHERE StudentID = @StudentID;",
                req.GetSql());
        }

        [Fact]
        public void ReturnStudentById_GetParameters_ProjectsId()
        {
            var req = new ReturnStudentById(987);

            var p = req.GetParameters()!;
            var id = (int)p.GetType().GetProperty("StudentID")!.GetValue(p)!;

            Assert.Equal(987, id);
        }

        [Fact]
        public void ReturnStudentById_InvalidId_Throws()
        {
            Assert.ThrowsAny<ArgumentException>(() => new ReturnStudentById(0));
            Assert.ThrowsAny<ArgumentException>(() => new ReturnStudentById(-1));
        }
    }
}
