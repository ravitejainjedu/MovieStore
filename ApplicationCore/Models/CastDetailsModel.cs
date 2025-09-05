namespace ApplicationCore.Models;

public class CastDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePath { get; set; }
    public List<(int movieId, string title, string? posterUrl, string? character)> Movies { get; set; } = new();
}
