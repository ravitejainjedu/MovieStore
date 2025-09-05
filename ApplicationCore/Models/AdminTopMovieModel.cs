namespace ApplicationCore.Models;

public class AdminTopMovieModel
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int TotalPurchases { get; set; }
    public string? PosterUrl { get; set; }
}
