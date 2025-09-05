namespace ApplicationCore.Entities;

public class MovieCrew
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;

    public int CrewId { get; set; }
    public Crew Crew { get; set; } = default!;

    public string? Job { get; set; }
}
