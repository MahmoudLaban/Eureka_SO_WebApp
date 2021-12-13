using Microsoft.EntityFrameworkCore;

namespace ReviewService.Models
{
    //From Entity Framework to implement ORM -https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
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
