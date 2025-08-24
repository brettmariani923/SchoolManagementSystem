namespace Teachers.Application.DTO
{
    public sealed class UpdateCourseRequest
    {
        public string CourseName { get; set; } = "";
        public int Credits { get; set; }
        public int SchoolID { get; set; }
    }
}
