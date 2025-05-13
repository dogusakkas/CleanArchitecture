using Application.Services;
using Domain.Entities;
using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarFeatures.Queries.GetAllCar
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, PaginationResult<Car>>
    {
        private readonly ICarService _carService;
        public GetAllCarQueryHandler(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<PaginationResult<Car>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Car> cars = await _carService.GetAllAsync(request, cancellationToken);
            return cars;
        }
    }
}
