using BigOn.Domain.Business.BlogPostModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BigOn.WebUI.AppCode.ViewComponents
{
    public class RelatedPostViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public RelatedPostViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var query =  new BlogPostRelatedQuery() { Size = 4, PostId = id};
            var post = await mediator.Send(query);
            return View(post);
        }
    }
}
