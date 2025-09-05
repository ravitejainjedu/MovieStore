namespace ApplicationCore.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
