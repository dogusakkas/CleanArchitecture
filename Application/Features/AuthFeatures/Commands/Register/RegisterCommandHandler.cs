﻿using Application.Services;
using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authService.RegisterAsync(request);
            return new("Kullanıcı kaydı tamamlandı");
        }
    }
}
