using Application.Features.CarFeatures.Commands.CreateCar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICarService
    {
        Task CreateAsync(CreateCarCommand createCarCommand, CancellationToken cancellationToken);
    }
}
