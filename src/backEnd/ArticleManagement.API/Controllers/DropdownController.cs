using Core.Application.DTOs;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        [HttpGet("statuses")]
        public ActionResult<IEnumerable<GenericCategoryDto>> GetDropdownStatuses()
        {
            IEnumerable<RequestStatus> statuses = RequestStatusExtension.GetAll();

            return Ok(statuses.Select(s => new GenericCategoryDto(s)).ToArray());
        }

        [HttpGet("articles")]
        public ActionResult<IEnumerable<GenericCategoryDto>> GetDropdownArticles()
        {
            IEnumerable<Article> articles = ArticleExtension.GetAll();

            return Ok(articles.Select(s => new GenericCategoryDto(s)).ToArray());
        }
    }
}
