using Teachers.Domain.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Rows;
using Teachers.Data.Requests.Teachers.Return;
using Teachers.Data.Requests.Teachers.Insert;
using Teachers.Data.Requests.Teachers.Update;
using Teachers.Data.Requests.Teachers.Remove;

namespace Teachers.Application.Services
{
    public sealed class TeacherService : ITeacherService
    {
        private readonly IDataAccess _data;
        public TeacherService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs
        public async Task<Teachers_DTO?> GetByIdAsync(int teacherId, CancellationToken ct = default)
        {
            var row = await _data.FetchAsync(new ReturnTeacherByID(teacherId));
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Teachers_DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnAllTeachers());
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int teacherId, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveTeacherByID(teacherId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> teacherIds, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveBulkTeachers(teacherIds));

        // Updates: DTO -> Row
        public Task<int> UpdateAsync(Teachers_DTO teacher, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateTeacher(MapToRow(teacher)));

        public Task<int> UpdateBulkAsync(IEnumerable<Teachers_DTO> teachers, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateBulkTeachers(teachers.Select(MapToRow)));

        // Inserts
        public Task<int> InsertAsync(Teachers_DTO newTeacher, CancellationToken ct = default)
             => _data.ExecuteAsync(new InsertNewTeacher(MapToRowForInsert(newTeacher)));

        public Task<int> InsertBulkAsync(IEnumerable<Teachers_DTO> newTeachers, int schoolID, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertBulkNewTeachers(
                newTeachers.Select(MapToRowForInsert), schoolID));

        // Mapping helpers 
        private static Teachers_DTO Map(Teachers_Row r) => new Teachers_DTO
        {
            TeacherID = r.TeacherID,
            FirstName = r.FirstName,
            LastName = r.LastName,
            SchoolID = r.SchoolID
        };

        private static Teachers_Row MapToRow(Teachers_DTO d) => new Teachers_Row
        {
            TeacherID = d.TeacherID,
            FirstName = d.FirstName,
            LastName = d.LastName,
            SchoolID = d.SchoolID
        };

        // For inserts: omit identity key
        private static Teachers_Row MapToRowForInsert(Teachers_DTO d) => new Teachers_Row
        {
            FirstName = d.FirstName,
            LastName = d.LastName,
            SchoolID = d.SchoolID
        };
    }
}
