using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface IEnrollmentService
    {
        // Reads
        Task<Enrollments_DTO?> GetByIdAsync(int enrollmentId, CancellationToken ct = default);
        Task<IEnumerable<Enrollments_DTO>> GetAllAsync(CancellationToken ct = default);
        Task<IEnumerable<Enrollments_DTO>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
        Task<IEnumerable<Enrollments_DTO>> GetByCourseIdAsync(int courseId, CancellationToken ct = default);
        Task<IEnumerable<Enrollments_DTO>> GetByTeacherIdAsync(int teacherId, CancellationToken ct = default);

        // Deletes
        Task<int> RemoveByIdAsync(int enrollmentId, CancellationToken ct = default);
        Task<int> RemoveBulkAsync(IEnumerable<int> enrollmentIds, CancellationToken ct = default);

        // Updates
        Task<int> UpdateAsync(Enrollments_DTO enrollment, CancellationToken ct = default);
        Task<int> UpdateBulkAsync(IEnumerable<Enrollments_DTO> enrollments, CancellationToken ct = default);

        // Inserts (EnrollmentID ignored if IDENTITY)
        Task<int> InsertAsync(EnrollmentRequest newEnrollment, CancellationToken ct = default);
        Task<int> InsertBulkAsync(IEnumerable<EnrollmentRequest> requests, CancellationToken ct = default);
    }
}
