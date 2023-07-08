using AutoMapper;
using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Business.BlogPostModule;
using BigOn.Domain.Models.Dtos.BlogPosts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigOn.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IValidator<BlogPostPostCommand> blogPostPostCommandValidator;
        private readonly IValidator<BlogPostPutCommand> blogPostPutComandValidator;

        public BlogPostsController(IMediator mediator, IMapper mapper, IConfiguration configuration,
            IValidator<BlogPostPostCommand> blogPostPostCommandValidator, IValidator<BlogPostPutCommand> blogPostPutComandValidator)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.configuration = configuration;
            this.blogPostPostCommandValidator = blogPostPostCommandValidator;
            this.blogPostPutComandValidator = blogPostPutComandValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] BlogPostGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            var modelDto = mapper.Map<PagedViewModel<BlogPostDto>>(response, cfg =>
            {
                cfg.Items.Add("uploadsFolder", configuration["uploads:link"]);
                cfg.Items.AddFromHeader(Request, "dateFormat");
            });

            return Ok(modelDto);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetSingle([FromRoute] BlogPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<BlogPostDto>(response, cfg =>
            {
                cfg.Items.Add("uploadsFolder", configuration["uploads:link"]);
                cfg.Items.AddFromHeader(Request, "dateFormat");
            });

            return Ok(modelDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BlogPostPostCommand command)
        {
            var validateResult = await blogPostPostCommandValidator.ValidateAsync(command);

            if (validateResult.IsValid)
            {
                var response = await mediator.Send(command);

                return Ok(response);
            }

            return BadRequest(validateResult);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] BlogPostPutCommand command)
        {
            var validateResult = await blogPostPutComandValidator.ValidateAsync(command);

            if (validateResult.IsValid)
            {
                var response = await mediator.Send(command);

                return Ok(response);
            }
            return BadRequest(validateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromHeader] BlogPostRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
