using Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using Application.Features.AuthFeatures.Commands.Login;
using Application.Features.AuthFeatures.Commands.Register;

namespace Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterCommand registerCommand);
        Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken);
        Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken);
    }
}
