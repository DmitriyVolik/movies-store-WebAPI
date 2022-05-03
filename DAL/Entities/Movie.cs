using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAL.Entities;

public class Movie
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    
    public List<MovieGenre> Genres { get; set; }

    [Required]
    public Director Director { get; set; }

    public DateTime ReleaseDate { get; set; }
}