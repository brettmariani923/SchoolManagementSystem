namespace Teachers.Data.Requests.Students
{
    public class ReturnAllStudents
    {
        public string GetSql()
        {
            return "SELECT Students FROM SchoolSystem;";
        }

        public object? GetParameters()
        {
            return null;
        }
    }
}
