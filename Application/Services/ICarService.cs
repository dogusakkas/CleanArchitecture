using Application.Features.CarFeatures.Commands.CreateCar;
using Application.Features.CarFeatures.Queries.GetAllCar;
using Domain.Entities;
using EntityFrameworkCorePagination.Nuget.Pagination;
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
        Task<PaginationResult<Car>> GetAllAsync(GetAllCarQuery request, CancellationToken cancellationToken);
    }
}
