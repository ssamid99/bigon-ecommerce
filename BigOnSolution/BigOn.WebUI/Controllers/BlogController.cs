using BigOn.Domain.Business.BlogPostModule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BigOn.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(BlogPostGetAllQuery query)
        {
           var response = await mediator.Send(query);
            return View(response);
        }
        [Route("/blog/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(BlogPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);

            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }
    }
}
