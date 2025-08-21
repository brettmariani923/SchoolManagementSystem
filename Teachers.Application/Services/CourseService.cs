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

        public async Task<Courses_DTO?> GetByIdAsync(int courseId, CancellationToken ct = default)
        {
            var row = await _data.FetchAsync(new ReturnCourseByID(courseId));
            if (row is null) return null;

            return new Courses_DTO
            {
                CourseID = row.CourseID,
                CourseName = row.CourseName,
                Credits = row.Credits,
                SchoolID = row.SchoolID
            };
        }

        public async Task<IEnumerable<Courses_DTO>> GetAllAsync(CancellationToken ct = default)
        {
            var rows = await _data.FetchListAsync(new ReturnAllCourses());
            return rows.Select(r => new Courses_DTO
            {
                CourseID = r.CourseID,
                CourseName = r.CourseName,
                Credits = r.Credits,
                SchoolID = r.SchoolID
            });
        }

        public Task<int> RemoveByIdAsync(int courseId, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveCourseByID(courseId));

        public Task<int> RemoveBulkAsync(IEnumerable<int> courseIds, CancellationToken ct = default)
            => _data.ExecuteAsync(new RemoveBulkCourses(courseIds));

        public Task<int> UpdateAsync(Courses_DTO course, CancellationToken ct = default)
            => _data.ExecuteAsync(
         new UpdateCourse(course.CourseID, course.CourseName, course.Credits, course.SchoolID));

        public Task<int> UpdateBulkAsync(IEnumerable<Courses_DTO> courses, CancellationToken ct = default)
        {
            var rows = courses.Select(c => new Courses_Row
            {
                CourseID = c.CourseID,
                CourseName = c.CourseName,
                Credits = c.Credits,
                SchoolID = c.SchoolID
            });

            return _data.ExecuteAsync(new UpdateBulkCourses(rows));
        }

        public Task<int> InsertAsync(Courses_DTO newCourse, CancellationToken ct = default)
            => _data.ExecuteAsync(
                new InsertNewCourse(newCourse.CourseName, newCourse.Credits, newCourse.SchoolID));

        public Task<int> InsertBulkAsync(IEnumerable<Courses_DTO> newCourses, CancellationToken ct = default)
        {
            var rows = newCourses.Select(c => new Courses_Row
            {
                CourseName = c.CourseName,
                Credits = c.Credits,
                SchoolID = c.SchoolID
            });

            return _data.ExecuteAsync(new InsertBulkNewCourses(rows));
        }
    }
}
