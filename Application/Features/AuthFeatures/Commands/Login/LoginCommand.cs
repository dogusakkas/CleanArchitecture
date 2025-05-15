using MediatR;

namespace Application.Features.AuthFeatures.Commands.Login
{
    public sealed record LoginCommand(string userNameorEmail, string password) : IRequest<LoginCommandResponse>
    {

    }
}
