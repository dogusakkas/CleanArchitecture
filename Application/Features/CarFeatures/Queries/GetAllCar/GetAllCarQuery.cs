using Domain.Entities;
using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;

namespace Application.Features.CarFeatures.Queries.GetAllCar
{
    public sealed record GetAllCarQuery(int PageNumber = 1, int PageSize = 10, string Search = "") : IRequest<PaginationResult<Car>>
    {
    }
}
