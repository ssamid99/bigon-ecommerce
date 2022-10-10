using BigOn.Domain.Business.BrandModule;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]BrandGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]BrandGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BrandPostCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody]BrandPutCommand command)
        {
            command.id = id;
            var response = await mediator.Send(command);
            if(response == null)
            {
                return NotFound();
            }
            
            return Ok(command);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]BrandDeleteCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
