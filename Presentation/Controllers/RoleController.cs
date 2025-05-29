using Application.Features.RoleFeatures.Commands.CreateRole;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public sealed class RoleController : ApiController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
