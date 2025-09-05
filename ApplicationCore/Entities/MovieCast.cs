using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class MovieCast
{
    public int CastId { get; set; }
    public Cast Cast { get; set; } = default!;

    [Required, StringLength(450)]
    public string Character { get; set; } = string.Empty;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
}
