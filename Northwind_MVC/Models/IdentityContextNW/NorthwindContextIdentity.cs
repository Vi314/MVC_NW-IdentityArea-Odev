using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Northwind_MVC.Models.IdentityContextNW
{
    public class NorthwindContextIdentity:IdentityDbContext
    {
        public NorthwindContextIdentity(DbContextOptions<NorthwindContextIdentity> options) :base(options)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
