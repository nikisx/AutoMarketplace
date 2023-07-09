using AutoMarketplace.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMarketplace.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public int SaveChanges(string userId)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added));

            foreach (var entity in entities)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    ((BaseEntity)entity.Entity).CreatedById = userId;
                }
                ((BaseEntity)entity.Entity).Created = System.DateTime.Now.AddHours(3);

            }

            return base.SaveChanges();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added));

            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).Created = System.DateTime.Now.AddHours(3);
            }

            return base.SaveChanges();
        }
    }
}