using Microsoft.EntityFrameworkCore;

namespace ReviewService.Models
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
        public DbSet<Review> Reviews { get; set; } = null!;
    }
}
