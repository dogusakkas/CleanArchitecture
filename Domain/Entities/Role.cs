using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public sealed class Role : IdentityRole<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
