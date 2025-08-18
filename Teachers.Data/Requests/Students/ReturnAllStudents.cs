namespace Teachers.Data.Requests.Students
{
    public class ReturnAllStudents
    {
        public string GetSql() =>
            "SELECT * FROM dbo.Students;";

        public object? GetParameters() => null;
    }
}
