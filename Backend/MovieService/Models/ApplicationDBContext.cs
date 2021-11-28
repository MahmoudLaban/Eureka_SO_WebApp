using Microsoft.EntityFrameworkCore;

namespace MovieService.Models
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
