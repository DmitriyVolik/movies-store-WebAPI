using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels;

public class UserRequestViewModel
{
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(16)]
    [MinLength(4)]
    public string Name { get; set; }
    
    public string Password { get; set; }
}