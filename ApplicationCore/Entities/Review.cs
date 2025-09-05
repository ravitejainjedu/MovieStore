using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Review
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    [Column(TypeName = "decimal(3,2)")]
    public decimal Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
