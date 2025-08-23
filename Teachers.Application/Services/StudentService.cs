using Teachers.Domain.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Rows;
using Teachers.Data.Requests.Students.Return;
using Teachers.Data.Requests.Students.Remove;
using Teachers.Data.Requests.Students.Update;
using Teachers.Data.Requests.Students.Insert;

namespace Teachers.Data.Services
{
    public sealed class StudentService : IStudentServices
    {
        private readonly IDataAccess _data;
        public StudentService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs
        public async Task<Students_DTO?> GetByIdAsync(int studentId, CancellationToken ct = default)
        {
            var row = await _data.FetchAsync(new ReturnStudentByID(studentId));
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Students_DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnAllStudents());
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int studentId, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveStudentByID(studentId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> studentIds, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveBulkStudents(studentIds));

        // Updates: DTO -> Row
        public Task<int> UpdateAsync(Students_DTO student, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateStudent(MapToRow(student)));

        public Task<int> UpdateBulkAsync(IEnumerable<Students_DTO> students, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateBulkStudents(students.Select(MapToRow)));

        // Inserts
        public Task<int> InsertAsync(Students_DTO newStudent, int schoolID, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertNewStudent(
                newStudent.FirstName, newStudent.LastName, newStudent.Year, schoolID));

        public Task<int> InsertBulkAsync(IEnumerable<Students_DTO> newStudents, int schoolID, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertBulkStudents(
                newStudents.Select(MapToRowForInsert), schoolID));

        // Mapping helpers
        private static Students_DTO Map(Students_Row r) => new Students_DTO
        {
            StudentID = r.StudentID,
            FirstName = r.FirstName,
            LastName = r.LastName,
            Year = r.Year,
            SchoolID = r.SchoolID
        };

        private static Students_Row MapToRow(Students_DTO d) => new Students_Row
        {
            StudentID = d.StudentID,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Year = d.Year,
            SchoolID = d.SchoolID
        };

        // For inserts: omit identity key
        private static Students_Row MapToRowForInsert(Students_DTO d) => new Students_Row
        {
            FirstName = d.FirstName,
            LastName = d.LastName,
            Year = d.Year,
            SchoolID = d.SchoolID
        };
    }
}
