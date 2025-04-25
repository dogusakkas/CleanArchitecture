using FluentValidation;

namespace Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().WithMessage("Araç adı boş olamaz");
            RuleFor(x=>x.Name).MinimumLength(3).WithMessage("Araç adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Model).NotEmpty().NotNull().WithMessage("Araç modeli boş olamaz");
            RuleFor(x => x.Model).MinimumLength(3).WithMessage("Araç modeli en az 3 karakter olmalıdır.");

            RuleFor(x => x.EnginePower).NotEmpty().NotNull().WithMessage("Araç motor gücü boş olamaz");
            RuleFor(x => x.EnginePower).MinimumLength(3).WithMessage("Araç motor gücü en az 3 karakter olmalıdır.");


        }
    }
}
