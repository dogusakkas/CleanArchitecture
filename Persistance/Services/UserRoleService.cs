using Application.Features.UserRoleFeatures.Commands;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork, IUserRoleRepository userRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
        }

        public async Task CreateAsync(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            UserRole userRole = new()
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            };

            await _userRoleRepository.AddAsync(userRole, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
