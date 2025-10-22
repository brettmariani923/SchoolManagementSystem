using Teachers.Data.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Rows;
using Teachers.Data.Requests.Students.Return;
using Teachers.Data.Requests.Students.Remove;
using Teachers.Data.Requests.Students.Update;
using Teachers.Data.Requests.Students.Insert;

namespace Teachers.Application.Services
{
    public sealed class StudentService : IStudentService
    {
        private readonly IDataAccess _data;
        public StudentService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs
        public async Task<Students_DTO?> GetByIdAsync(int studentId)
        {
            var row = await _data.FetchAsync(new ReturnStudentByID(studentId));
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Students_DTO>> GetAllAsync( )
        {
            var rows = await _data.FetchListAsync(new ReturnAllStudents());
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int studentId)
            => _data.ExecuteAsync(new RemoveStudentByID(studentId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> studentIds)
            => _data.ExecuteAsync(new RemoveBulkStudents(studentIds));

        // Updates: DTO -> Row
        public Task<int> UpdateAsync(Students_DTO student)
            => _data.ExecuteAsync(new UpdateStudent(MapToRow(student)));

        public Task<int> UpdateBulkAsync(IEnumerable<Students_DTO> students)
            => _data.ExecuteAsync(new UpdateBulkStudents(students.Select(MapToRow)));

        // Inserts
        public Task<int> InsertAsync(StudentRequest newStudent)
            => _data.ExecuteAsync(new InsertNewStudent(MapToRowForInsert(newStudent)));

        public Task<int> InsertBulkAsync(IEnumerable<StudentRequest> newStudents)
            => _data.ExecuteAsync(new InsertBulkStudents(newStudents.Select(MapToRowForInsert)));

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
        private static Students_Row MapToRowForInsert(StudentRequest d) => new Students_Row
        {
            FirstName = d.FirstName,
            LastName = d.LastName,
            Year = d.Year,
            SchoolID = d.SchoolID
        };
    }
}
