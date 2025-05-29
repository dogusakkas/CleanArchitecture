using Application.Features.UserRoleFeatures.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserRoleService
    {
        Task CreateAsync(CreateUserRoleCommand request, CancellationToken cancellationToken);
    }
}
