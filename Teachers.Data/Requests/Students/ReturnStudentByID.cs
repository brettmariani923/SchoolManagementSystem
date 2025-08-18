namespace Teachers.Data.Requests.Students
{
    public class ReturnStudentById
    {
        private readonly int _studentID;

        public ReturnStudentById(int studentID)
        {
            _studentID = studentID;
        }

        public string GetSql() =>
            "SELECT StudentID, FirstName, LastName, [Year], SchoolID " +
            "FROM dbo.Students " +
            "WHERE StudentID = @StudentID;";

        public object GetParameters() => new { StudentID = _studentID };
    }
}
