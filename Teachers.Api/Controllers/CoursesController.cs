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
        public async Task<ActionResult<IEnumerable<Courses_DTO>>> GetAll()
            => Ok(await _courses.GetAllAsync());

        // GET: api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Courses_DTO>> GetById(int id)
        {
            var course = await _courses.GetByIdAsync(id);
            return course is null ? NotFound() : Ok(course);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CourseRequest request)
        {
            if (request is null) return BadRequest("Body required.");

            var rows = await _courses.InsertAsync(request);
            if (rows <= 0) return Problem("Insert failed.");

            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/courses/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<CourseRequest> requests)
        {
            if (requests is null) return BadRequest("Body required.");

            var rows = await _courses.InsertBulkAsync(requests);
            if (rows <= 0) return Problem("Bulk insert failed.");

            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/courses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseRequest body)
        {
            if (id <= 0) return BadRequest("Invalid id.");

            var dto = new Courses_DTO
            {
                CourseID = id,
                CourseName = body.CourseName,
                Credits = body.Credits,
                SchoolID = body.SchoolID
            };

            var rows = await _courses.UpdateAsync(dto);
            return rows == 0 ? NotFound() : NoContent();
        }

        // PUT: api/courses/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> BulkUpdate([FromBody] IEnumerable<Courses_DTO> dtos)
        {
            if (dtos is null || !dtos.Any())
                return BadRequest("At least one course is required.");

            if (dtos.Any(c => c.CourseID <= 0))
                return BadRequest("Each course must include a valid CourseID.");

            if (dtos.Select(c => c.CourseID).Distinct().Count() != dtos.Count())
                return BadRequest("Duplicate CourseIDs are not allowed.");

            var rows = await _courses.UpdateBulkAsync(dtos);

            return rows <= 0
                ? Problem("Bulk update failed.")
                : NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid id.");

            var rows = await _courses.RemoveByIdAsync(id);
            if (rows == 0) return NotFound();

            return NoContent();
        }

        // DELETE: api/courses/bulk
        [HttpDelete("bulk")]
        public async Task<ActionResult> BulkDelete([FromQuery] IEnumerable<int> ids)
        {
            if (ids is null || !ids.Any()) return BadRequest("At least one id is required.");

            var rows = await _courses.RemoveBulkAsync(ids);
            if (rows <= 0) return Problem("Bulk delete failed.");

            return NoContent();
        }
    }
}

