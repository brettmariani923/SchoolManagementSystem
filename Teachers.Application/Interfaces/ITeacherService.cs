using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface ITeacherService
    {
        // Reads
        Task<Teachers_DTO?> GetByIdAsync(int teacherId, CancellationToken ct = default);
        Task<IEnumerable<Teachers_DTO>> GetAllAsync(CancellationToken ct = default);

        // Deletes
        Task<int> RemoveByIdAsync(int teacherId, CancellationToken ct = default);
        Task<int> RemoveBulkAsync(IEnumerable<int> teacherIds, CancellationToken ct = default);

        // Updates
        Task<int> UpdateAsync(Teachers_DTO teacher, CancellationToken ct = default);
        Task<int> UpdateBulkAsync(IEnumerable<Teachers_DTO> teachers, CancellationToken ct = default);

        // Inserts (TeacherID on DTO is ignored if DB uses IDENTITY)
        Task<int> InsertAsync(TeacherRequest newTeacher, CancellationToken ct = default);
        Task<int> InsertBulkAsync(IEnumerable<TeacherRequest> newTeachers, CancellationToken ct = default);
    }
}
