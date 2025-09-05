namespace ApplicationCore.Models;

public class PurchaseDetailsModel
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? PosterUrl { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public Guid PurchaseNumber { get; set; }
}
