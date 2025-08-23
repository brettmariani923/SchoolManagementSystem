using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class StudentsController : ControllerBase
    {
        private readonly IStudentService _students;

        public StudentsController(IStudentService students)
        {
            _students = students;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students_DTO>>> GetAll(CancellationToken ct)
        {
            var list = await _students.GetAllAsync(ct);
            return Ok(list);
        }

        // GET: api/students/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Students_DTO>> GetById(int id, CancellationToken ct)
        {
            var student = await _students.GetByIdAsync(id, ct);
            return student is null ? NotFound() : Ok(student);
        }

        // POST /api/schools/{schoolId}/students
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Students_DTO dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest("Body required.");
            if (dto.SchoolID <= 0) return BadRequest("Valid SchoolID is required.");

            var rows = await _students.InsertAsync(dto, ct);  // <- only (dto, ct)
            if (rows <= 0) return Problem("Insert failed.");

            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/students/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Students_DTO dto, CancellationToken ct)
        {
            if (dto is null) return BadRequest("Body required.");
            if (id <= 0) return BadRequest("Invalid id.");
            dto.StudentID = id;

            var rows = await _students.UpdateAsync(dto, ct);
            if (rows == 0) return NotFound();
            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            if (id <= 0) return BadRequest("Invalid id.");
            var rows = await _students.RemoveByIdAsync(id, ct);
            if (rows == 0) return NotFound();
            return NoContent();
        }

        // POST: api/students/bulk?schoolID=12
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<Students_DTO> dtos, [FromQuery] int schoolID, CancellationToken ct)
        {
            if (dtos is null) return BadRequest("Body required.");
            if (schoolID <= 0) return BadRequest("Valid schoolID is required.");

            var rows = await _students.InsertBulkAsync(dtos, schoolID, ct);
            if (rows <= 0) return Problem("Bulk insert failed.");
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/students/bulk
        [HttpPut("bulk")]
        public async Task<ActionResult> BulkUpdate([FromBody] IEnumerable<Students_DTO> dtos, CancellationToken ct)
        {
            if (dtos is null) return BadRequest("Body required.");
            var rows = await _students.UpdateBulkAsync(dtos, ct);
            if (rows <= 0) return Problem("Bulk update failed.");
            return NoContent();
        }

        // DELETE: api/students/bulk?ids=1&ids=2
        [HttpDelete("bulk")]
        public async Task<ActionResult> BulkDelete([FromQuery] IEnumerable<int> ids, CancellationToken ct)
        {
            if (ids is null || !ids.Any()) return BadRequest("At least one id is required.");
            var rows = await _students.RemoveBulkAsync(ids, ct);
            if (rows <= 0) return Problem("Bulk delete failed.");
            return NoContent();
        }
    }
}

