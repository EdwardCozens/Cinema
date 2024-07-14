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

            modelBuilder.Entity<Show>()
                .HasKey(s => new { s.MovieId, s.TheatreId });

            modelBuilder.Entity<Show>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Shows)
                .HasForeignKey(s => s.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
                .HasOne(s => s.Theatre)
                .WithMany(t => t.Shows)
                .HasForeignKey(s => s.TheatreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Theatre> Theatres { get; set; }
    }
}
