using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ComputerStore.Infrastructure.Data;

namespace ComputerStore.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

           
            var connectionString = "Server=localhost;Database=ComputerStoreDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
