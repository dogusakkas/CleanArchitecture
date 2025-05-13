using Application.Features.CarFeatures.Commands.CreateCar;
using Application.Features.CarFeatures.Queries.GetAllCar;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using EntityFrameworkCorePagination.Nuget.Pagination;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarService(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork, ICarRepository carRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
        }

        public async Task CreateAsync(CreateCarCommand createCarCommand, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(createCarCommand);

            await _carRepository.AddAsync(car, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //throw new Exception("Manual test exception from CreateAsync");

        }

        public async Task<PaginationResult<Car>> GetAllAsync(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Car> cars = await _carRepository
                .Where(x => x.Name.ToLower().Contains(request.Search.ToLower()))
                .OrderBy(x=>x.Name)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            return cars;
        }
    }
}
