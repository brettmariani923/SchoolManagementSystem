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

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StudentRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest("Body required.");

            var rows = await _students.InsertAsync(request, ct);
            if (rows <= 0) return Problem("Insert failed.");

            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/students/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<StudentRequest> requests, CancellationToken ct)
        {
            if (requests is null) return BadRequest("Body required.");

            var rows = await _students.InsertBulkAsync(requests, ct);
            if (rows <= 0) return Problem("Bulk insert failed.");

            return CreatedAtAction(nameof(GetAll), null);
        }

        // STUDENTS: PUT api/students/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentRequest body, CancellationToken ct)
        {
            if (id <= 0) return BadRequest("Invalid id.");

            var dto = new Students_DTO
            {
                StudentID = id,                  
                FirstName = body.FirstName,
                LastName = body.LastName,
                Year = body.Year,
                SchoolID = body.SchoolID
            };

            var rows = await _students.UpdateAsync(dto, ct);
            return rows == 0 ? NotFound() : NoContent();
        }

        // PUT: api/students/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> BulkUpdate([FromBody] IEnumerable<Students_DTO> dtos, CancellationToken ct)
        {
            if (dtos is null || !dtos.Any())
                return BadRequest("At least one student is required.");

            if (dtos.Any(s => s.StudentID <= 0))
                return BadRequest("Each student must include a valid StudentID.");

            if (dtos.Select(s => s.StudentID).Distinct().Count() != dtos.Count())
                return BadRequest("Duplicate StudentIDs are not allowed.");

            var rows = await _students.UpdateBulkAsync(dtos, ct);

            return rows <= 0
                ? Problem("Bulk update failed.")
                : NoContent();
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

        // DELETE: api/students/bulk
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

