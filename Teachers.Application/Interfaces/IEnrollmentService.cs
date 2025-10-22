using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface IEnrollmentService
    {
        // Reads
        Task<Enrollments_DTO?> GetByIdAsync(int enrollmentId);
        Task<IEnumerable<Enrollments_DTO>> GetAllAsync( );
        Task<IEnumerable<Enrollments_DTO>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<Enrollments_DTO>> GetByCourseIdAsync(int courseId);
        Task<IEnumerable<Enrollments_DTO>> GetByTeacherIdAsync(int teacherId);

        // Deletes
        Task<int> RemoveByIdAsync(int enrollmentId);
        Task<int> RemoveBulkAsync(IEnumerable<int> enrollmentIds);

        // Updates
        Task<int> UpdateAsync(Enrollments_DTO enrollment);
        Task<int> UpdateBulkAsync(IEnumerable<Enrollments_DTO> enrollments);

        // Inserts 
        Task<int> InsertAsync(EnrollmentRequest newEnrollment);
        Task<int> InsertBulkAsync(IEnumerable<EnrollmentRequest> requests);
    }
}
