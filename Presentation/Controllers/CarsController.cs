using Application.Features.CarFeatures.Commands.CreateCar;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstraction;

namespace Presentation.Controllers
{
    public sealed class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateCarCommand createCarCommand, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(createCarCommand, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult gettest()
        {
            int x = 0;
            int y = 0;
            int sonuc = x / y;
            return Ok();
        }
    }
}
