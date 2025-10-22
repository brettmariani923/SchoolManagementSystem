using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teachers_DTO>> GetById(int id)
        {
            var teacher = await _service.GetByIdAsync(id);
            if (teacher is null)
                return NotFound();

            return Ok(teacher);
        }

        // GET: api/teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teachers_DTO>>> GetAll()
        {
            var teachers = await _service.GetAllAsync();
            return Ok(teachers);
        }

        // POST: api/teacher
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] TeacherRequest request)
        {
            if (request is null)
                return BadRequest("Body required.");

            if (request.SchoolID <= 0)
                return BadRequest("Valid SchoolID is required.");

            var rows = await _service.InsertAsync(request);
            if (rows <= 0)
                return Problem("Insert failed.");

            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/teacher/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> InsertBulk([FromBody] IEnumerable<TeacherRequest> requests)
        {
            if (requests is null)
                return BadRequest("Body required.");

            var rows = await _service.InsertBulkAsync(requests);
            if (rows <= 0)
                return Problem("Bulk insert failed.");

            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/teacher/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherRequest body)
        {
            if (id <= 0) return BadRequest("Invalid id.");
            if (body is null) return BadRequest("Body required.");

            var dto = new Teachers_DTO
            {
                TeacherID = id,
                FirstName = body.FirstName,
                LastName = body.LastName,
                SchoolID = body.SchoolID
            };

            var rows = await _service.UpdateAsync(dto);
            return rows == 0 ? NotFound() : NoContent();
        }

        // PUT: api/teacher/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBulk([FromBody] IEnumerable<Teachers_DTO> dtos)
        {
            if (dtos is null || !dtos.Any())
                return BadRequest("At least one teacher is required.");

            if (dtos.Any(t => t.TeacherID <= 0))
                return BadRequest("Each teacher must include a valid TeacherID.");

            if (dtos.Select(t => t.TeacherID).Distinct().Count() != dtos.Count())
                return BadRequest("Duplicate TeacherIDs are not allowed.");

            var rows = await _service.UpdateBulkAsync(dtos);
            return rows <= 0 ? Problem("Bulk update failed.") : NoContent();
        }

        // DELETE: api/teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            await _service.RemoveByIdAsync(id);
            return NoContent();
        }

        // DELETE: api/teacher/bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] IEnumerable<int> teacherIds)
        {
            await _service.RemoveBulkAsync(teacherIds);
            return NoContent();
        }
    }
}
