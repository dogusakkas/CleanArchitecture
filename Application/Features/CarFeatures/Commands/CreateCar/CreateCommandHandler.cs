using Domain.Dtos;
using MediatR;

namespace Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCommandHandler: IRequestHandler<CreateCarCommand, MessageResponse>
    {
        public async Task<MessageResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {

            return new("Araç başarıyla üretildi");
        }
    }
}
