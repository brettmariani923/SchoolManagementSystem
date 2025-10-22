using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface ICourseService
    {
        // Reads
        Task<Courses_DTO?> GetByIdAsync(int courseId);
        Task<IEnumerable<Courses_DTO>> GetAllAsync();

        // Deletes
        Task<int> RemoveByIdAsync(int courseId);
        Task<int> RemoveBulkAsync(IEnumerable<int> courseIds);

        // Updates
        Task<int> UpdateAsync(Courses_DTO course);
        Task<int> UpdateBulkAsync(IEnumerable<Courses_DTO> courses);

        // Inserts
        Task<int> InsertAsync(CourseRequest newCourse);
        Task<int> InsertBulkAsync(IEnumerable<CourseRequest> newCourses);
    }
}
