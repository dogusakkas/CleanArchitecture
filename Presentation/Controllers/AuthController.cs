using Application.Features.AuthFeatures.Commands.Register;
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
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            MessageResponse result = await _mediator.Send(registerCommand, cancellationToken);
            return Ok(result);
        }
    }
}
