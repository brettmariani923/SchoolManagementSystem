using Microsoft.AspNetCore.Mvc;
using Teachers.Application.DTO;
using Teachers.Application.Interfaces;

namespace Teachers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/enrollment/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Enrollments_DTO>> GetById(int id)
        {
            var enrollment = await _service.GetByIdAsync(id);
            return enrollment is null ? NotFound() : Ok(enrollment);
        }

        // GET: api/enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // GET: api/enrollment/by-student/12
        [HttpGet("by-student/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByStudentId(int studentId)
        {
            var list = await _service.GetByStudentIdAsync(studentId);
            return Ok(list);
        }

        // GET: api/enrollment/by-course/34
        [HttpGet("by-course/{courseId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByCourseId(int courseId)
        {
            var list = await _service.GetByCourseIdAsync(courseId);
            return Ok(list);
        }

        // GET: api/enrollment/by-teacher/7
        [HttpGet("by-teacher/{teacherId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollments_DTO>>> GetByTeacherId(int teacherId)
        {
            var list = await _service.GetByTeacherIdAsync(teacherId);
            return Ok(list);
        }

        // POST: api/enrollments
        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] EnrollmentRequest newEnrollment)
        {
            if (newEnrollment is null) return BadRequest("Body required.");

            var id = await _service.InsertAsync(newEnrollment);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // POST: api/enrollments/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> BulkInsert([FromBody] IEnumerable<EnrollmentRequest> requests)
        {
            if (requests is null) return BadRequest("Body required.");

            var rows = await _service.InsertBulkAsync(requests);
            if (rows <= 0) return Problem("Bulk insert failed.");

            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: api/enrollment/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnrollmentRequest body)
        {
            if (id <= 0) return BadRequest("Invalid id.");
            if (body is null) return BadRequest("Body required.");

            var dto = new Enrollments_DTO
            {
                EnrollmentID = id, // route is source of truth
                StudentID = body.StudentID,
                CourseID = body.CourseID,
                TeacherID = body.TeacherID,
                SchoolID = body.SchoolID,
            };

            var rows = await _service.UpdateAsync(dto);
            return rows == 0 ? NotFound() : NoContent();
        }

        // PUT: api/enrollment/bulk
        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBulk([FromBody] IEnumerable<Enrollments_DTO> dtos)
        {
            if (dtos is null || !dtos.Any())
                return BadRequest("At least one enrollment is required.");

            if (dtos.Any(e => e.EnrollmentID <= 0))
                return BadRequest("Each enrollment must include a valid EnrollmentID.");

            if (dtos.Select(e => e.EnrollmentID).Distinct().Count() != dtos.Count())
                return BadRequest("Duplicate EnrollmentIDs are not allowed.");

            var rows = await _service.UpdateBulkAsync(dtos);
            return rows <= 0 ? Problem("Bulk update failed.") : NoContent();
        }

        // DELETE: api/enrollment/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            await _service.RemoveByIdAsync(id);
            return NoContent();
        }

        // DELETE: api/enrollment/bulk
        [HttpDelete("bulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] IEnumerable<int> enrollmentIds)
        {
            if (enrollmentIds is null || !enrollmentIds.Any())
                return BadRequest("At least one enrollmentId is required.");

            await _service.RemoveBulkAsync(enrollmentIds);
            return NoContent();
        }
    }
}
