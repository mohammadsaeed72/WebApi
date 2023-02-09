using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleWebApi._0.Helpers;
using SampleWebApi._1.Entities;

namespace SampleWebApi._2.Database
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var entitiesAssembly = typeof(AppUser).Assembly;

            builder.RegisterAllEntities<IEntity>(entitiesAssembly);

            builder.ApplyConfigurationsFromAssembly(entitiesAssembly);

        }
    }
}
