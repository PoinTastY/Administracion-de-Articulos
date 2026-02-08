using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly IExtendService _extendService;
        private readonly ILogger<ExtensionController> _logger;

        public ExtensionController(IExtendService extendService, ILogger<ExtensionController> logger)
        {
            _extendService = extendService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddExtensionRequest([FromBody] ExtendRequestDto request)
        {
            int createdId = await _extendService.CreateExtendRequestAsync(request);

            if (createdId <= 0)
            {
                _logger.LogError("Failed to create extension request for student code {StudentCode}", request.StudentCode);
                return StatusCode(500, "Error creating extension request.");
            }

            request = request with { Id = createdId };

            _logger.LogDebug("Extension request created with ID {ExtendId} for student code {StudentCode}: {@Request}", createdId, request.StudentCode, request);

            _logger.LogInformation("Created extension request with ID {ExtendId} for student code {StudentCode}", createdId, request.StudentCode);
            return CreatedAtAction(nameof(GetExtensionRequestById), new { extendId = createdId }, request);
        }

        [HttpGet("{extendId}")]
        public async Task<IActionResult> GetExtensionRequestById(int extendId)
        {
            var request = await _extendService.GetByIdAsync(extendId);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        //I need a metod to upload the evidence file to a blob storage, and then save the file URL in the database. I will use Azure Blob Storage for this purpose.
        [HttpPost("{extendId}/upload-evidence")]
        public async Task<IActionResult> UploadEvidence(int extendId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using Stream fileStream = file.OpenReadStream();
            string fileUrl = await _extendService.UploadEvidenceAsync(extendId, fileStream, file.FileName, file.ContentType);

            if (string.IsNullOrEmpty(fileUrl))
            {
                return StatusCode(500, "Error uploading file.");
            }

            // Save the file URL in the database
            bool isSaved = await _extendService.SaveEvidenceUrlAsync(extendId, fileUrl);

            if (!isSaved)
            {
                return StatusCode(500, "Error saving evidence URL.");
            }

            return Ok(new { FileUrl = fileUrl });
        }
    }
}
