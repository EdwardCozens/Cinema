using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Movies)
                .WithOne(e => e.Genre)
                .HasForeignKey(e => e.GenreId)
                .IsRequired();
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}
