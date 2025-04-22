using Application.Services;
using Domain.Dtos;
using MediatR;

namespace Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCommandHandler: IRequestHandler<CreateCarCommand, MessageResponse>
    {
        private readonly ICarService _carService;

        public CreateCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<MessageResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            await _carService.CreateAsync(request, cancellationToken);
            return new("Araç başarıyla üretildi");
        }
    }
}
