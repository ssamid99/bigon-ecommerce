using BigOn.Domain.Business.BlogPostModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BigOn.WebUI.AppCode.ViewComponents
{
    public class RecentPostViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public RecentPostViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query =  new BlogPostRecentQuery() { Size = 4 };
            var post = await mediator.Send(query);
            return View(post);
        }
    }
}
