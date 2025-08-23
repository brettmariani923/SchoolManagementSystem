using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/enrollment/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Enrollments_DTO>> GetById(int id, CancellationToken ct)
        {
            var enrollment = await _service.GetByIdAsync(id, ct);
            return enrollment is null ? NotFound() : Ok(enrollment); // GetByIdAsync
        }

        // GET: api/enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetAll(CancellationToken ct)
        {
            var list = await _service.GetAllAsync(ct); // GetAllAsync
            return Ok(list);
        }

        // GET: api/enrollment/by-student/12
        [HttpGet("by-student/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByStudentId(int studentId, CancellationToken ct)
        {
            var list = await _service.GetByStudentIdAsync(studentId, ct); // GetByStudentIdAsync
            return Ok(list);
        }

        // GET: api/enrollment/by-course/34
        [HttpGet("by-course/{courseId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByCourseId(int courseId, CancellationToken ct)
        {
            var list = await _service.GetByCourseIdAsync(courseId, ct); // GetByCourseIdAsync
            return Ok(list);
        }

        // GET: api/enrollment/by-teacher/7
        [HttpGet("by-teacher/{teacherId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByTeacherId(int teacherId, CancellationToken ct)
        {
            var list = await _service.GetByTeacherIdAsync(teacherId, ct); // GetByTeacherIdAsync
            return Ok(list);
        }

        // DELETE: api/enrollment/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveById(int id, CancellationToken ct)
        {
            await _service.RemoveByIdAsync(id, ct); // RemoveByIdAsync
            return NoContent();
        }

        // DELETE: api/enrollment/bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] IEnumerable<int> enrollmentIds, CancellationToken ct)
        {
            if (enrollmentIds is null || !enrollmentIds.Any())
                return BadRequest("At least one enrollmentId is required.");

            await _service.RemoveBulkAsync(enrollmentIds, ct); // RemoveBulkAsync
            return NoContent();
        }

        // PUT: api/enrollment/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Enrollments_DTO enrollment, CancellationToken ct)
        {
            if (enrollment is null) return BadRequest("Body required.");
            if (id != enrollment.EnrollmentID) return BadRequest("Mismatched EnrollmentID.");

            await _service.UpdateAsync(enrollment, ct); // UpdateAsync
            return NoContent();
        }

        // PUT: api/enrollment/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBulk([FromBody] IEnumerable<Enrollments_DTO> enrollments, CancellationToken ct)
        {
            if (enrollments is null || !enrollments.Any())
                return BadRequest("At least one enrollment is required.");

            await _service.UpdateBulkAsync(enrollments, ct); // UpdateBulkAsync
            return NoContent();
        }

        // POST: api/enrollment
        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] Enrollments_DTO newEnrollment, CancellationToken ct)
        {
            if (newEnrollment is null) return BadRequest("Body required.");

            var id = await _service.InsertAsync(newEnrollment, ct); // InsertAsync
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // POST: api/enrollment/bulk?teacherId=7&courseId=55&schoolId=3
        // Body: [101,102,103]  (studentIds)
        [HttpPost("bulk")]
        public async Task<ActionResult<int>> InsertBulk(
            [FromBody] IEnumerable<int> studentIds,
            [FromQuery] int teacherId,
            [FromQuery] int courseId,
            [FromQuery] int schoolId,
            CancellationToken ct)
        {
            if (studentIds is null || !studentIds.Any())
                return BadRequest("At least one studentId is required.");
            if (teacherId <= 0 || courseId <= 0 || schoolId <= 0)
                return BadRequest("Valid teacherId, courseId, and schoolId are required.");

            var count = await _service.InsertBulkAsync(studentIds, teacherId, courseId, schoolId, ct); // InsertBulkAsync
            return Ok(count);
        }
    }
}
