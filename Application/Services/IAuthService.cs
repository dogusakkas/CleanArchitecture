using Application.Features.AuthFeatures.Commands.Register;

namespace Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterCommand registerCommand);
    }
}
