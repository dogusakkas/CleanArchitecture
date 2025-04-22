using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed record CreateCarCommand (string Name, string Model, string EnginePower) : IRequest<MessageResponse>;
}
