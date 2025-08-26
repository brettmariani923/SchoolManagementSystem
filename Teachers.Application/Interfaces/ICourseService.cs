using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface ICourseService
    {
        // Reads
        Task<Courses_DTO?> GetByIdAsync(int courseId, CancellationToken ct = default);
        Task<IEnumerable<Courses_DTO>> GetAllAsync(CancellationToken ct = default);

        // Deletes
        Task<int> RemoveByIdAsync(int courseId, CancellationToken ct = default);
        Task<int> RemoveBulkAsync(IEnumerable<int> courseIds, CancellationToken ct = default);

        // Updates
        Task<int> UpdateAsync(Courses_DTO course, CancellationToken ct = default);
        Task<int> UpdateBulkAsync(IEnumerable<Courses_DTO> courses, CancellationToken ct = default);

        // Inserts (CourseID on DTO is ignored if DB uses IDENTITY)
        Task<int> InsertAsync(CourseRequest newCourse, CancellationToken ct = default);
        Task<int> InsertBulkAsync(IEnumerable<CourseRequest> newCourses, CancellationToken ct = default);
    }
}
