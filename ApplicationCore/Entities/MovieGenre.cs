namespace ApplicationCore.Entities;

public class MovieGenre
{
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = default!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
}
