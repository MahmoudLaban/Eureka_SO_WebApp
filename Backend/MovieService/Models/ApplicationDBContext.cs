﻿using Microsoft.EntityFrameworkCore;

namespace MovieService.Models

    // ORM -https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/
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
