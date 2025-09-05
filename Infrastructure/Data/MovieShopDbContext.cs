using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Trailer> Trailers => Set<Trailer>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Cast> Casts => Set<Cast>();
    public DbSet<MovieCast> MovieCasts => Set<MovieCast>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Crew> Crews => Set<Crew>();
    public DbSet<MovieCrew> MovieCrews => Set<MovieCrew>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
        modelBuilder.Entity<MovieCast>().HasKey(mc => new { mc.MovieId, mc.CastId });
        modelBuilder.Entity<MovieCrew>().HasKey(mc => new { mc.MovieId, mc.CrewId });
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<Favorite>().HasKey(f => new { f.MovieId, f.UserId });

        // relationships
        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie).WithMany(m => m.MovieGenres).HasForeignKey(mg => mg.MovieId);
        modelBuilder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);

        modelBuilder.Entity<MovieCast>()
            .HasOne(mc => mc.Movie).WithMany(m => m.MovieCasts).HasForeignKey(mc => mc.MovieId);
        modelBuilder.Entity<MovieCast>()
            .HasOne(mc => mc.Cast).WithMany(c => c.MovieCasts).HasForeignKey(mc => mc.CastId);

        modelBuilder.Entity<MovieCrew>()
            .HasOne(mc => mc.Movie).WithMany(m => m.MovieCrews).HasForeignKey(mc => mc.MovieId);
        modelBuilder.Entity<MovieCrew>()
            .HasOne(mc => mc.Crew).WithMany(c => c.MovieCrews).HasForeignKey(mc => mc.CrewId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
    }
}
