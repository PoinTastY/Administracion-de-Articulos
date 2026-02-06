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

        public ExtensionController(IExtendService extendService)
        {
            _extendService = extendService;
        }

        [HttpPost]
        public async Task<IActionResult> AddExtensionRequest([FromBody] ExtendRequestDto request)
        {
            int createdId = await _extendService.CreateExtendRequestAsync(request);
            return CreatedAtAction(nameof(GetExtensionRequestById), new { id = createdId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExtensionRequestById(int id)
        {
            var request = await _extendService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }
    }
}
