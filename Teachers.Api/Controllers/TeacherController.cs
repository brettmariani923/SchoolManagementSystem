using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        //GET: api/teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teachers_DTO>> GetById(int id, CancellationToken ct)
        {
            var teacher = await _service.GetByIdAsync(id, ct);
            if (teacher is null)
                return NotFound();
            return Ok(teacher);
        }

        //GET: api/teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teachers_DTO>>> GetAll(CancellationToken ct)
        {
            var teachers = await _service.GetAllAsync(ct);
            return Ok(teachers);
        }

        //DELETE: api/teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById(int id, CancellationToken ct)
        {
            await _service.RemoveByIdAsync(id, ct);
            return NoContent();
        }

        //DELETE: api/teacher/bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] IEnumerable<int> teacherIds, CancellationToken ct)
        {
            await _service.RemoveBulkAsync(teacherIds, ct);
            return NoContent();
        }

        //PUT: api/teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teachers_DTO teacher, CancellationToken ct)
        {
            if (id != teacher.TeacherID)
                return BadRequest("Mismatched TeacherID.");

            await _service.UpdateAsync(teacher, ct);
            return NoContent();
        }

        //PUT: api/teacher/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBulk([FromBody] IEnumerable<Teachers_DTO> teachers, CancellationToken ct)
        {
            await _service.UpdateBulkAsync(teachers, ct);
            return NoContent();
        }

        //POST: api/teacher?schoolID=5
        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] Teachers_DTO newTeacher, int schoolID, CancellationToken ct)
        {
            var id = await _service.InsertAsync(newTeacher, schoolID, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        //POST: api/teacher/bulk?schoolID=5
        [HttpPost("bulk")]
        public async Task<ActionResult<int>> InsertBulk([FromBody] IEnumerable<Teachers_DTO> newTeachers, int schoolID, CancellationToken ct)
        {
            var count = await _service.InsertBulkAsync(newTeachers, schoolID, ct);
            return Ok(count);
        }
    }
}
