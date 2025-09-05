using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;

    [Required, StringLength(2084)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(2084)]
    public string TrailerUrl { get; set; } = string.Empty;
}
