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

        public CoursesController(ICourseService courses)
        {
            _courses = courses;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses_DTO>>> GetAll(CancellationToken ct)
        {
            var list = await _courses.GetAllAsync(ct);
            return Ok(list);
        }

        // GET: api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Courses_DTO>> GetById(int id, CancellationToken ct)
        {
            var course = await _courses.GetByIdAsync(id, ct);
            return course is null ? NotFound() : Ok(course);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Courses_DTO dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest("Body required.");
            // ID is identity; ignore dto.CourseID on insert
            var rows = await _courses.InsertAsync(dto, ct);
            if (rows <= 0) return Problem("Insert failed.");

            // If you want to return the created resource, fetch it or return location if you know it.
            // Here we just return 201 with a location to GetAll (or swap to GetById if you retrieve new ID).
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/courses/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Courses_DTO dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest("Body required.");
            if (id <= 0) return BadRequest("Invalid id.");
            dto.CourseID = id;

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

        // POST: api/courses/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<Courses_DTO> dtos, CancellationToken ct)
        {
            if (dtos is null) return BadRequest("Body required.");
            var rows = await _courses.InsertBulkAsync(dtos, ct);
            if (rows <= 0) return Problem("Bulk insert failed.");
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/courses/bulk
        [HttpPut("bulk")]
        public async Task<ActionResult> BulkUpdate([FromBody] IEnumerable<Courses_DTO> dtos, CancellationToken ct)
        {
            if (dtos is null) return BadRequest("Body required.");
            var rows = await _courses.UpdateBulkAsync(dtos, ct);
            if (rows <= 0) return Problem("Bulk update failed.");
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
