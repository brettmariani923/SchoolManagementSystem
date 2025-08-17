namespace Teachers.Data.Requests.Teachers
{
    public class ReturnAllTeachers
    {
        public string GetSql()
        {
            return "SELECT Teachers FROM SchoolSystem;";
        }

        public object? GetParameters()
        {
            return null;
        }
    }
}
