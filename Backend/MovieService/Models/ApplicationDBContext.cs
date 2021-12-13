using Microsoft.EntityFrameworkCore;

namespace MovieService.Models

// From Entity Framework to implement ORM -https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; } = null!;
    }
}
