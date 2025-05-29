using Application.Features.RoleFeatures.Commands.CreateRole;
using Application.Features.UserRoleFeatures.Commands;
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
    public sealed class UserRolesController : ApiController
    {
        public UserRolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
