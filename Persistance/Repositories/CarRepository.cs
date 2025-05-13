using Domain.Entities;
using Domain.Repositories;
using GenericRepository;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public sealed class CarRepository : Repository<Car, AppDbContext>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }
    }
}
