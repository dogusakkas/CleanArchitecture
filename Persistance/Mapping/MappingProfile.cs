using Application.Features.AuthFeatures.Commands.Register;
using Application.Features.CarFeatures.Commands.CreateCar;
using AutoMapper;
using Domain.Entities;

namespace Persistance.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCarCommand, Car>();
            CreateMap<RegisterCommand, User>();
        }
    }
}
