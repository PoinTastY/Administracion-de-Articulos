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

                _logger.LogInformation("Student created/updated successfully with ID {StudentId}.", resultingKey);
                return CreatedAtAction(nameof(CreateStudent), new { extendId = resultingKey }, studentDto);
            }

            _logger.LogError("Failed to create or update student with ID {StudentId}.", studentDto.Id);
            return BadRequest("Could not create or update the student.");
        }
    }
}
