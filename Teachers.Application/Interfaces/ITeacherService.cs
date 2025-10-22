using Teachers.Application.DTO;

namespace Teachers.Application.Interfaces
{
    public interface ITeacherService
    {
        // Reads
        Task<Teachers_DTO?> GetByIdAsync(int teacherId);
        Task<IEnumerable<Teachers_DTO>> GetAllAsync();

        // Deletes
        Task<int> RemoveByIdAsync(int teacherId);
        Task<int> RemoveBulkAsync(IEnumerable<int> teacherIds);

        // Updates
        Task<int> UpdateAsync(Teachers_DTO teacher);
        Task<int> UpdateBulkAsync(IEnumerable<Teachers_DTO> teachers);

        // Inserts 
        Task<int> InsertAsync(TeacherRequest newTeacher);
        Task<int> InsertBulkAsync(IEnumerable<TeacherRequest> newTeachers);
    }
}
