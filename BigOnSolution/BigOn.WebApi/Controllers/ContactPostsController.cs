
using AutoMapper;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Business.ContactPostModule;
using BigOn.Domain.Models.Dtos.ContactPosts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigOn.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IValidator<ContactPostPostCommand> contactPostPostValidator;

        public ContactPostsController(IMediator mediator, IMapper mapper, IValidator<ContactPostPostCommand> contactPostPostValidator)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.contactPostPostValidator = contactPostPostValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] ContactPostGetAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<List<ContactPostDto>>(response);

            return Ok(modelDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] ContactPostGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<ContactPostDto>(response);

            return Ok(modelDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactPostPostCommand command)
        {
            var validateResult = await contactPostPostValidator.ValidateAsync(command);

            if (validateResult.IsValid)
            {
                var response = await mediator.Send(command);

                return Ok(response);
            }
            return BadRequest(validateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] ContactPostDeleteCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }

            var modelDto = mapper.Map<ContactPostDto>(response);

            return Ok(modelDto);
        }
    }
}
