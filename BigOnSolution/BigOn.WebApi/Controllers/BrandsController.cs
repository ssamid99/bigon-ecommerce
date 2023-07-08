using AutoMapper;
using BigOn.Domain.Business.BrandModule;
using BigOn.Domain.Models.Dtos.Brands;
using BigOn.Domain.Validators.BrandValidators;
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
    public class BrandsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IValidator<BrandPostCommand> brandCreateCommandValidator;
        private readonly IValidator<BrandPutCommand> brandPutCommandValidator;

        public BrandsController(IMediator mediator, IMapper mapper,
            IValidator<BrandPostCommand> brandCreateCommandValidator,
            IValidator<BrandPutCommand> brandPutCommandValidator)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.brandCreateCommandValidator = brandCreateCommandValidator;
            this.brandPutCommandValidator = brandPutCommandValidator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] BrandGetAllQuery query)
        {
            var response = await mediator.Send(query);
            var dtoModel = mapper.Map<List<BrandDto>>(response);
            return Ok(dtoModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] BrandGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            var dtoModel = mapper.Map<BrandDto>(response);
            return Ok(dtoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BrandPostCommand command)
        {
            var validateResult = brandCreateCommandValidator.Validate(command);

            if (validateResult.IsValid)
            {
                var response = await mediator.Send(command);
                var dtoModel = mapper.Map<BrandDto>(response);
                return Ok(dtoModel);
            }
            return BadRequest(validateResult);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BrandPutCommand command)
        {
            var validateResult = await brandPutCommandValidator.ValidateAsync(command);

            if (validateResult.IsValid)
            {
                var response = await mediator.Send(command);
                if (response == null)
                {
                    return NotFound();
                }
                var dtoModel = mapper.Map<BrandDto>(response);

                return Ok(dtoModel);
            }
            return BadRequest(validateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] BrandDeleteCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            var dtoModel = mapper.Map<BrandDto>(response);

            return Ok(dtoModel);
        }
    }
}
