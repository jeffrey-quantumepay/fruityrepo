using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using FruityViceApi.Application.Commands;
using FruityViceApi.Application.Models;
using FruityViceApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FruityViceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruityViceController : ControllerBase
    {
        private readonly ILogger<FruityViceController> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public FruityViceController(
            ILogger<FruityViceController> logger,
            IMediator mediator,
            IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet("{name}", Name = "GetFruitByName")]
        public async Task<IActionResult> Get([FromRoute]  string name)
        {
            try
            {
                var response = await _mediator.Send(new GetFruitCommand()
                {
                    name = name,
                });

                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{name}", Name = "UpdateFruit")]
        public async Task<IActionResult> UpdateFruit([FromRoute] string name, [FromBody] Fruit fruit)
        {
            try
            {
                var response = await _mediator.Send(new UpdateFruitMetadataCommand()
                {
                    model = fruit,
                });

                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
