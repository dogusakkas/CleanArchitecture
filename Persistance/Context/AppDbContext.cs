using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public sealed class AppDbContext : DbContext // sealed başka bir classın miras almasını engeller
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entires = ChangeTracker.Entries<Entity>();
            foreach (var item in entires)
            {
                if (item.State == EntityState.Added)
                {
                    item.Property(x => x.CreatedDate).CurrentValue = DateTime.Now;
                }

                if (item.State == EntityState.Modified)
                {
                    item.Property(x => x.UpdatedDate).CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
