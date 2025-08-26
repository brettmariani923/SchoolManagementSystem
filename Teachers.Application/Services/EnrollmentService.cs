using Teachers.Domain.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Rows;
using Teachers.Data.Requests.Enrollments.Return;
using Teachers.Data.Requests.Enrollments.Insert;
using Teachers.Data.Requests.Enrollments.Update;
using Teachers.Data.Requests.Enrollments.Remove;

namespace Teachers.Application.Services
{
    public sealed class EnrollmentService : IEnrollmentService
    {
        private readonly IDataAccess _data;
        public EnrollmentService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs
        public async Task<Enrollments_DTO?> GetByIdAsync(int enrollmentId, CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnEnrollmentsByEnrollmentID(enrollmentId));
            var row = rows.FirstOrDefault();
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Enrollments_DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnAllEnrollments());
            return rows.Select(Map);
        }

        public async Task<IEnumerable<Enrollments_DTO>> GetByStudentIdAsync(int studentId, CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnEnrollmentsByStudentID(studentId));
            return rows.Select(Map);
        }

        public async Task<IEnumerable<Enrollments_DTO>> GetByCourseIdAsync(int courseId, CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnEnrollmentsByCourseID(courseId));
            return rows.Select(Map);
        }

        public async Task<IEnumerable<Enrollments_DTO>> GetByTeacherIdAsync(int teacherId, CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnEnrollmentsByTeacherID(teacherId));
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int enrollmentId, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveStudentEnrollmentByID(enrollmentId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> enrollmentIds, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveBulkStudentEnrollments(enrollmentIds));

        // Updates (DTO -> Row -> row-based request)
        public Task<int> UpdateAsync(Enrollments_DTO enrollment, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateStudentEnrollment(MapToRow(enrollment)));

        public Task<int> UpdateBulkAsync(IEnumerable<Enrollments_DTO> enrollments, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateBulkEnrollments(enrollments.Select(MapToRow)));

        // Inserts (DTO -> Row -> row-based request)
        public Task<int> InsertAsync(EnrollmentRequest newEnrollment, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertStudentEnrollment(MapToRowForInsert(newEnrollment)));

        // Bulk insert (studentIds -> Rows -> row-based request)
        public Task<int> InsertBulkAsync(IEnumerable<EnrollmentRequest> newCourses, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertBulkStudentEnrollment(newCourses.Select(MapToRowForInsert)));

        // Mapping helpers
        private static Enrollments_DTO Map(Enrollments_Row r) => new Enrollments_DTO
        {
            EnrollmentID = r.EnrollmentID,
            StudentID = r.StudentID,
            TeacherID = r.TeacherID,
            CourseID = r.CourseID,
            SchoolID = r.SchoolID
        };

        private static Enrollments_Row MapToRow(Enrollments_DTO d) => new Enrollments_Row
        {
            EnrollmentID = d.EnrollmentID,
            StudentID = d.StudentID,
            TeacherID = d.TeacherID,
            CourseID = d.CourseID,
            SchoolID = d.SchoolID
        };
        private static Enrollments_Row MapToRowForInsert(EnrollmentRequest d) => new Enrollments_Row
        {
            StudentID = d.StudentID,
            TeacherID = d.TeacherID,
            CourseID = d.CourseID,
            SchoolID = d.SchoolID
        };
    }
}
