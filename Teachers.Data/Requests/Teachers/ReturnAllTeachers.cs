namespace Teachers.Data.Requests.Teachers
{
    public class ReturnAllTeachers
    {
        public string GetSql() =>
            "SELECT * FROM dbo.Teachers;";

        public object? GetParameters() => null;
    }
}
