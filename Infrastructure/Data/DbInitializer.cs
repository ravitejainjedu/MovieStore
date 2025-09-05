using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task EnsureSeededAsync(MovieShopDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (!await db.Genres.AnyAsync())
        {
            db.Genres.AddRange(
                new Genre { Id = 1, Name = "ACTION" },
                new Genre { Id = 2, Name = "DRAMA" },
                new Genre { Id = 3, Name = "SCIENCE FICTION" },
                new Genre { Id = 4, Name = "ADVENTURE" },
                new Genre { Id = 5, Name = "COMEDY" }
            );
        }

        if (!await db.Movies.AnyAsync())
        {
            var m1 = new Movie { Id = 1, Title = "Inception", Overview = "A mind-bending thriller.", PosterUrl = "https://image.tmdb.org/t/p/w342/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg", Revenue = 829895144, RunTime = 148, ReleaseDate = new DateTime(2010,7,15) };
            var m2 = new Movie { Id = 2, Title = "Interstellar", Overview = "Exploration through space and time.", PosterUrl = "https://image.tmdb.org/t/p/w342/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", Revenue = 701729206, RunTime = 169, ReleaseDate = new DateTime(2014,11,7) };
            db.Movies.AddRange(m1, m2);

            db.MovieGenres.AddRange(
                new MovieGenre { MovieId = 1, GenreId = 1 },
                new MovieGenre { MovieId = 1, GenreId = 3 },
                new MovieGenre { MovieId = 2, GenreId = 3 },
                new MovieGenre { MovieId = 2, GenreId = 4 }
            );

            db.Trailers.AddRange(
                new Trailer { Id = 1, MovieId = 1, Name = "Trailer 1", TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0" },
                new Trailer { Id = 2, MovieId = 2, Name = "Trailer 1", TrailerUrl = "https://www.youtube.com/watch?v=zSWdZVtXT7E" }
            );

            var leo = new Cast { Id = 1, Name = "Leonardo DiCaprio", ProfilePath = "https://image.tmdb.org/t/p/w185/A85WIRIKVsD2DeUSc8wQ4fOKc4e.jpg" };
            var matt = new Cast { Id = 2, Name = "Matthew McConaughey", ProfilePath = "https://image.tmdb.org/t/p/w185/4X9CwQKQ9N3kWeItPxX2VINqb0Z.jpg" };
            db.Casts.AddRange(leo, matt);

            db.MovieCasts.AddRange(
                new MovieCast { MovieId = 1, CastId = 1, Character = "Dom Cobb" },
                new MovieCast { MovieId = 2, CastId = 2, Character = "Cooper" }
            );

            var user = new User { Id = 1, Email = "user1@example.com", FirstName = "John", LastName = "Doe", HashedPassword = "hashed", Salt = "salt" };
            db.Users.Add(user);

            
            // Purchases
            if (!await db.Purchases.AnyAsync())
            {
                db.Purchases.AddRange(
                    new Purchase { Id = 1, UserId = 1, MovieId = 1, PurchaseDateTime = DateTime.UtcNow.AddDays(-10), PurchaseNumber = Guid.NewGuid(), TotalPrice = 12.99m },
                    new Purchase { Id = 2, UserId = 1, MovieId = 2, PurchaseDateTime = DateTime.UtcNow.AddDays(-5), PurchaseNumber = Guid.NewGuid(), TotalPrice = 12.99m }
                );
            }

            await db.SaveChangesAsync();
        }
    }
}
