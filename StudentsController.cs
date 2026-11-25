using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Models;
using StudentManagementApi.Services;

namespace StudentManagementApi.Controllers
{
    /// <summary>
    /// RESTful API controller for performing CRUD operations on Student resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// GET /api/students
        /// Returns all students, optionally filtered by department name.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetAll([FromQuery] string? department)
        {
            var students = _studentService.GetAll(department);
            return Ok(students);
        }

        /// <summary>
        /// GET /api/students/{id}
        /// Returns a single student matching the specified ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student is null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            return Ok(student);
        }

        /// <summary>
        /// POST /api/students
        /// Creates a new student record after validating the request payload.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> Create([FromBody] Student student)
        {
            var created = _studentService.Create(student);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// PUT /api/students/{id}
        /// Updates an existing student record by ID.
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> Update(int id, [FromBody] Student student)
        {
            var updated = _studentService.Update(id, student);
            if (updated is null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            return Ok(updated);
        }

        /// <summary>
        /// DELETE /api/students/{id}
        /// Removes a student record by ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleted = _studentService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            return NoContent();
        }

        /// <summary>
        /// GET /api/students/error-test
        /// Intentionally throws an exception to verify GlobalExceptionMiddleware.
        /// </summary>
        [HttpGet("error-test")]
        public IActionResult TriggerTestException()
        {
            throw new InvalidOperationException("Simulated server failure to demonstrate GlobalExceptionMiddleware.");
        }
    }
}
