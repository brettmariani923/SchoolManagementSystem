using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CoursesController : ControllerBase
    {
        private readonly ICourseService _courses;
        public CoursesController(ICourseService courses) => _courses = courses;

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses_DTO>>> GetAll(CancellationToken ct)
            => Ok(await _courses.GetAllAsync(ct));

        // GET: api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Courses_DTO>> GetById(int id, CancellationToken ct)
        {
            var course = await _courses.GetByIdAsync(id, ct);
            return course is null ? NotFound() : Ok(course);
        }

        // POST: api/courses  (NO CourseID in request)
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCourseRequest body, CancellationToken ct)
        {
            if (body is null) return BadRequest("Body required.");

            var dto = new Courses_DTO
            {
                // CourseID intentionally omitted (identity)
                CourseName = body.CourseName,
                Credits = body.Credits,
                SchoolID = body.SchoolID
            };

            var rows = await _courses.InsertAsync(dto, ct);
            if (rows <= 0) return Problem("Insert failed.");

            // If your service later returns the new ID, switch this to CreatedAtAction(GetById, new { id = newId }, null)
            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/courses/bulk  (NO CourseID in each item)
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<CreateCourseRequest> items, CancellationToken ct)
        {
            if (items is null) return BadRequest("Body required.");

            var dtos = items.Select(x => new Courses_DTO
            {
                CourseName = x.CourseName,
                Credits = x.Credits,
                SchoolID = x.SchoolID
            });

            var rows = await _courses.InsertBulkAsync(dtos, ct);
            if (rows <= 0) return Problem("Bulk insert failed.");
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/courses/5  (id in route; body has no ID)
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCourseRequest body, CancellationToken ct)
        {
            if (body is null) return BadRequest("Body required.");
            if (id <= 0) return BadRequest("Invalid id.");

            var dto = new Courses_DTO
            {
                CourseID = id,                   // set from route
                CourseName = body.CourseName,
                Credits = body.Credits,
                SchoolID = body.SchoolID
            };

            var rows = await _courses.UpdateAsync(dto, ct);
            if (rows == 0) return NotFound();
            return NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            if (id <= 0) return BadRequest("Invalid id.");
            var rows = await _courses.RemoveByIdAsync(id, ct);
            if (rows == 0) return NotFound();
            return NoContent();
        }

        // DELETE: api/courses/bulk?ids=1&ids=2
        [HttpDelete("bulk")]
        public async Task<ActionResult> BulkDelete([FromQuery] IEnumerable<int> ids, CancellationToken ct)
        {
            if (ids is null || !ids.Any()) return BadRequest("At least one id is required.");
            var rows = await _courses.RemoveBulkAsync(ids, ct);
            if (rows <= 0) return Problem("Bulk delete failed.");
            return NoContent();
        }
    }
}
