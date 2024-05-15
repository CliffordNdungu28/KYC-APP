using KYC_APP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KYC_APP.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DocumentTypes> DocumentTypes { get; set; }
        public DbSet<Models.File> Files { get; set; }

        public DbSet<AddedServiceProviders> AddedServiceProviders { get; set; }

        public DbSet<KYCRequirements> KYCRequirements { get; set; }


    }
}
