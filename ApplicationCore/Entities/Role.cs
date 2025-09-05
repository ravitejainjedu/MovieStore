using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Role
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
