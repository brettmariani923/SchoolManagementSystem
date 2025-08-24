namespace Teachers.Application.DTO
{
    public sealed class CreateCourseRequest
    {
        public string CourseName { get; set; } = "";
        public int Credits { get; set; }
        public int SchoolID { get; set; }
    }
}
