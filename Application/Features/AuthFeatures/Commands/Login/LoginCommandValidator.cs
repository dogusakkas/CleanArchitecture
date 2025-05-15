using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.userNameorEmail).NotEmpty().NotNull().WithMessage("Kullanıcı adı veya mail boş olamaz");
            RuleFor(x => x.userNameorEmail).MinimumLength(3).WithMessage("Kullanıcı adı veya mail en az 3 karakter olmalıdır");

            RuleFor(x => x.password).NotEmpty().NotNull().WithMessage("Şifre bilgisi boş olamaz");
            RuleFor(x => x.password).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir");
            RuleFor(x => x.password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir");
            RuleFor(x => x.password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir");
            RuleFor(x => x.password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir");
        }
    }
}
