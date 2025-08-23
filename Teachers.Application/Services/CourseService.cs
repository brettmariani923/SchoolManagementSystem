using Teachers.Data.Rows;
using Teachers.Domain.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Requests.Courses.Return;
using Teachers.Data.Requests.Courses.Insert;
using Teachers.Data.Requests.Courses.Update;
using Teachers.Data.Requests.Courses.Remove;

namespace Teachers.Data.Services
{
    public sealed class CourseService : ICourseService
    {
        private readonly IDataAccess _data;
        public CourseService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs 
        public async Task<Courses_DTO?> GetByIdAsync(int courseId, CancellationToken ct = default)
        {
            var row = await _data.FetchAsync(new ReturnCourseByID(courseId));
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Courses_DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnAllCourses());
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int courseId, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveCourseByID(courseId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> courseIds, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveBulkCourses(courseIds));

        // Updates: DTO -> Row 
        public Task<int> UpdateAsync(Courses_DTO course, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateCourse(MapToRow(course)));

        public Task<int> UpdateBulkAsync(IEnumerable<Courses_DTO> courses, CancellationToken ct = default)
            => _data.ExecuteAsync(new UpdateBulkCourses(courses.Select(MapToRow)));

        // Inserts: DTO -> Row using MapToRowForInsert
        public Task<int> InsertAsync(Courses_DTO newCourse, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertNewCourse(
                newCourse.CourseName, newCourse.Credits, newCourse.SchoolID));

        public Task<int> InsertBulkAsync(IEnumerable<Courses_DTO> newCourses, CancellationToken ct = default)
            => _data.ExecuteAsync(new InsertBulkNewCourses(newCourses.Select(MapToRowForInsert)));

        // Mapping helpers
        private static Courses_DTO Map(Courses_Row r) => new Courses_DTO
        {
            CourseID = r.CourseID,
            CourseName = r.CourseName,
            Credits = r.Credits,
            SchoolID = r.SchoolID
        };

        private static Courses_Row MapToRow(Courses_DTO d) => new Courses_Row
        {
            CourseID = d.CourseID,
            CourseName = d.CourseName,
            Credits = d.Credits,
            SchoolID = d.SchoolID
        };

        // For inserts: omit identity
        private static Courses_Row MapToRowForInsert(Courses_DTO d) => new Courses_Row
        {
            CourseName = d.CourseName,
            Credits = d.Credits,
            SchoolID = d.SchoolID
        };
    }
}
