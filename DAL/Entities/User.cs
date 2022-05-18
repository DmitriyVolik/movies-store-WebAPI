using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(16)]
    [MinLength(4)]
    public string Name { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Role { get; set; }
}