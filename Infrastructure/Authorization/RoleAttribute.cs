using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authorization
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleAttribute(string role, IUserRoleRepository userRoleRepository)
        {
            _role = role;
            _userRoleRepository = userRoleRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userHasRole = _userRoleRepository.Where(x => x.UserId == userIdClaim.Value).Include(x=>x.Role).Any(x=>x.Role.Name == _role);
            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
