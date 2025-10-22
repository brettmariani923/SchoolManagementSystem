// Teachers.Application.Interfaces
using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface IStudentService
    {
        // Reads
        Task<Students_DTO?> GetByIdAsync(int studentId);
        Task<IEnumerable<Students_DTO>> GetAllAsync();

        // Deletes
        Task<int> RemoveByIdAsync(int studentId);
        Task<int> RemoveBulkAsync(IEnumerable<int> studentIds);

        // Updates
        Task<int> UpdateAsync(Students_DTO student);
        Task<int> UpdateBulkAsync(IEnumerable<Students_DTO> students);

        // Inserts 
        Task<int> InsertAsync(StudentRequest newStudent);
        Task<int> InsertBulkAsync(IEnumerable<StudentRequest> newStudents);
    }
}
