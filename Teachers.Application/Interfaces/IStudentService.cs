// Teachers.Application.Interfaces
using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface IStudentService
    {
        // Reads
        Task<Students_DTO?> GetByIdAsync(int studentId, CancellationToken ct = default);
        Task<IEnumerable<Students_DTO>> GetAllAsync(CancellationToken ct = default);

        // Deletes
        Task<int> RemoveByIdAsync(int studentId, CancellationToken ct = default);
        Task<int> RemoveBulkAsync(IEnumerable<int> studentIds, CancellationToken ct = default);

        // Updates
        Task<int> UpdateAsync(Students_DTO student, CancellationToken ct = default);
        Task<int> UpdateBulkAsync(IEnumerable<Students_DTO> students, CancellationToken ct = default);

        // Inserts (schoolID enforced by controller/service)
        Task<int> InsertAsync(Students_DTO newStudent, CancellationToken ct = default);
        Task<int> InsertBulkAsync(IEnumerable<Students_DTO> newStudents, CancellationToken ct = default);
    }
}
