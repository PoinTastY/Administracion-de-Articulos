using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
        {
            int resultingKey = await _studentService.AddAsync(studentDto);

            if (resultingKey > 0)
            {
                studentDto = studentDto with { Id = resultingKey };

                _logger.LogInformation("Student created successfully with ID {StudentId}.", resultingKey);
                return CreatedAtAction(nameof(CreateStudent), new { id = resultingKey }, studentDto);
            }

            if (await _studentService.UpdateAsync(studentDto))
            {
                _logger.LogInformation("Student with ID {StudentId} updated successfully.", studentDto.Id);
                return Ok(new { student = studentDto, message = "Student updated successfully." });
            }

            _logger.LogError("Failed to create or update student with ID {StudentId}.", studentDto.Id);
            return BadRequest("Could not create or update the student.");
        }
    }
}
