﻿using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Register
{
    public sealed record RegisterCommand(string Email, string UserName, string NameLastName, string Password) : IRequest<MessageResponse>
    {
    }
}
