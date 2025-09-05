namespace ApplicationCore.Models;

public class MovieDetailsModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Overview { get; set; }
    public string? PosterUrl { get; set; }
    public decimal? Revenue { get; set; }
    public int? RunTime { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public List<string> Genres { get; set; } = new();
    public List<(string name, string url)> Trailers { get; set; } = new();
    public List<(int castId, string castName, string? character, string? profilePath)> Casts { get; set; } = new();
    public bool IsPurchased { get; set; }
    public decimal? Price { get; set; }
}
