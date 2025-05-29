using Application.Abstractions;
using Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using Application.Features.AuthFeatures.Commands.Login;
using Application.Features.AuthFeatures.Commands.Register;
using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IJwtProvider _jwtProvider;

        public AuthService(UserManager<User> userManager, IMapper mapper, IMailService mailService, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            if (user.RefreshToken != request.RefreshToken)
                throw new Exception("Refresh Token süresi geçerli değil");

            if (user.RefreshTokenExpires < DateTime.Now)
                throw new Exception("Refresh Token süresi dolmuş");

            LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);
            return response;
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.Users.Where(x => x.UserName == request.userNameorEmail || x.Email == request.userNameorEmail)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            var result = await _userManager.CheckPasswordAsync(user, request.password);

            if (result)
            {
                LoginCommandResponse loginCommandResponse = await _jwtProvider.CreateTokenAsync(user);
                return loginCommandResponse;
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }
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
