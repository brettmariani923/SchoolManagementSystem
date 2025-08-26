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
        public async Task<ActionResult> Create([FromBody] CourseRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest("Body required.");

            var rows = await _courses.InsertAsync(request, ct);
            if (rows <= 0) return Problem("Insert failed.");

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<CourseRequest> requests, CancellationToken ct)
        {
            if (requests is null) return BadRequest("Body required.");

            var rows = await _courses.InsertBulkAsync(requests, ct);
            if (rows <= 0) return Problem("Bulk insert failed.");
            return CreatedAtAction(nameof(GetAll), null);
        }

        // COURSES: PUT api/courses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseRequest body, CancellationToken ct)
        {
            if (id <= 0) return BadRequest("Invalid id.");

            var dto = new Courses_DTO
            {
                CourseID = id,                
                CourseName = body.CourseName,
                Credits = body.Credits,
                SchoolID = body.SchoolID
            };

            var rows = await _courses.UpdateAsync(dto, ct);
            return rows == 0 ? NotFound() : NoContent();
        }

        // COURSES: PUT api/courses/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> BulkUpdate([FromBody] IEnumerable<Courses_DTO> dtos, CancellationToken ct)
        {
            if (dtos is null || !dtos.Any())
                return BadRequest("At least one course is required.");

            if (dtos.Any(c => c.CourseID <= 0))
                return BadRequest("Each course must include a valid CourseID.");

            if (dtos.Select(c => c.CourseID).Distinct().Count() != dtos.Count())
                return BadRequest("Duplicate CourseIDs are not allowed.");

            var rows = await _courses.UpdateBulkAsync(dtos, ct);

            return rows <= 0
                ? Problem("Bulk update failed.")
                : NoContent();
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
