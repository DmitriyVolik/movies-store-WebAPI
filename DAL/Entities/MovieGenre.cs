using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class MovieGenre
{
    public int Id { get; set; }
    
    [Required]
    public Genre Genre { get; set; }
}