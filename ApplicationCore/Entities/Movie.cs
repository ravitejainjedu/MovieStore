using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Movie
{
    public int Id { get; set; }

    [Required, StringLength(256)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2048)]
    public string? PosterUrl { get; set; }

    [StringLength(2048)]
    public string? BackdropUrl { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Budget { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Revenue { get; set; }

    public int? RunTime { get; set; }

    [StringLength(2084)]
    public string? ImdbUrl { get; set; }

    [StringLength(2084)]
    public string? TmdbUrl { get; set; }

    [StringLength(64)]
    public string? OriginalLanguage { get; set; }

    [StringLength(512)]
    public string? Tagline { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Overview { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }

    public ICollection<Trailer> Trailers { get; set; } = new List<Trailer>();
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public ICollection<MovieCast> MovieCasts { get; set; } = new List<MovieCast>();
    public ICollection<MovieCrew> MovieCrews { get; set; } = new List<MovieCrew>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
