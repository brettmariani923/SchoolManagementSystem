using Teachers.Data.Rows;
using Teachers.Data.Interfaces;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;
using Teachers.Data.Requests.Courses.Return;
using Teachers.Data.Requests.Courses.Insert;
using Teachers.Data.Requests.Courses.Update;
using Teachers.Data.Requests.Courses.Remove;

namespace Teachers.Application.Services
{
    public sealed class CourseService : ICourseService
    {
        private readonly IDataAccess _data;
        public CourseService(IDataAccess data) => _data = data;

        // Reads: Rows -> DTOs 
        public async Task<Courses_DTO?> GetByIdAsync(int courseId)
        {
            var row = await _data.FetchAsync(new ReturnCourseByID(courseId));
            return row is null ? null : Map(row);
        }

        public async Task<IEnumerable<Courses_DTO>> GetAllAsync()
        {
            var rows = await _data.FetchListAsync(new ReturnAllCourses());
            return rows.Select(Map);
        }

        // Deletes
        public Task<int> RemoveByIdAsync(int courseId)
            => _data.ExecuteAsync(new RemoveCourseByID(courseId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> courseIds)
            => _data.ExecuteAsync(new RemoveBulkCourses(courseIds));

        // Updates: DTO -> Row 
        public Task<int> UpdateAsync(Courses_DTO course)
            => _data.ExecuteAsync(new UpdateCourse(MapToRow(course)));

        public Task<int> UpdateBulkAsync(IEnumerable<Courses_DTO> courses)
            => _data.ExecuteAsync(new UpdateBulkCourses(courses.Select(MapToRow)));

        // Inserts: DTO -> Row using MapToRowForInsert
        public Task<int> InsertAsync(CourseRequest newCourse)
            => _data.ExecuteAsync(new InsertNewCourse(MapToRowForInsert(newCourse)));

        public Task<int> InsertBulkAsync(IEnumerable<CourseRequest> newCourses)
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

        private static Courses_Row MapToRowForInsert(CourseRequest d) => new Courses_Row
        {
            CourseName = d.CourseName,
            Credits = d.Credits,
            SchoolID = d.SchoolID
        };
    }
}

