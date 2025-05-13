using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Mail bilgisi boş olamaz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz");

            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır");

            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Şifre bilgisi boş olamaz");
            RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir");
            RuleFor(x => x.Password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir");
            RuleFor(x => x.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir");
        }
    }
}
