namespace ApplicationCore.Entities;

public class Favorite
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
