using System.ComponentModel.DataAnnotations;
using BLL.Models.Enums;

namespace BLL.Models;

public class Movie
{
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<MovieGenre> Genres { get; set; }

    public Director Director { get; set; }

    public DateTime ReleaseDate { get; set; }
}