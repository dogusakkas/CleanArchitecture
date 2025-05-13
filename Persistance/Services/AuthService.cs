using Application.Features.AuthFeatures.Commands.Register;
using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public AuthService(UserManager<User> userManager, IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task RegisterAsync(RegisterCommand registerCommand)
        {
            User user = _mapper.Map<User>(registerCommand);
            IdentityResult result = await _userManager.CreateAsync(user, registerCommand.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            List<string> emails = new();
            emails.Add(registerCommand.Email);

            await _mailService.SendMailAsync(new SendMailDto
            {
                Subject = "Kayıt Başarılı",
                Body = $"Hoş geldiniz, Sayın {registerCommand.NameLastName}, kaydınız başarıyla tamamlandı.",
                Emails = new List<string> { registerCommand.Email }
            });
        }
    }
}
