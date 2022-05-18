using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public class UserModel
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }

    public string Name { get; set; }
    
    public string Role { get; set; }
}