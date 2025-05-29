using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserRoleFeatures.Commands
{
    public sealed class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
    {
        public CreateUserRoleCommandValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty().NotNull().WithMessage("Kullanıcı bilgisi boş olamaz");
            RuleFor(x => x.RoleId).NotEmpty().NotNull().WithMessage("Rol bilgisi boş olamaz");
        }
    }
}
