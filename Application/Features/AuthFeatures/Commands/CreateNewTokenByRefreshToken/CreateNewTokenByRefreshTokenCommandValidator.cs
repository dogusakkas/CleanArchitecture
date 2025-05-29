using FluentValidation;

namespace Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken
{
    public sealed class CreateNewTokenByRefreshTokenCommandValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
    {
        public CreateNewTokenByRefreshTokenCommandValidator()
        {
            RuleFor(x=>x.userId).NotEmpty().NotNull().WithMessage("Kullanıcı bilgisi boş olamaz");
            RuleFor(x=>x.RefreshToken).NotEmpty().NotNull().WithMessage("Refresh Token bilgisi boş olamaz");
        }
    }
}
