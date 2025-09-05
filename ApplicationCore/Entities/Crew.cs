using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Crew
{
    public int Id { get; set; }

    [Required, StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [StringLength(64)]
    public string? Department { get; set; }

    public ICollection<MovieCrew> MovieCrews { get; set; } = new List<MovieCrew>();
}
