using Application.Features.CarFeatures.Commands.CreateCar;
using Application.Features.CarFeatures.Queries.GetAllCar;
using Domain.Dtos;
using Domain.Entities;
using EntityFrameworkCorePagination.Nuget.Pagination;
using Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstraction;

namespace Presentation.Controllers
{
    
    public sealed class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator)
        {
        }

        //[TypeFilter(typeof(RoleAttribute), Arguments = new Object[] {"Create"})]
        [RoleFilter("Create")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateCarCommand createCarCommand, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(createCarCommand, cancellationToken);
            return Ok(response);
        }

        [RoleFilter("GetAll")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAll(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Car> response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
