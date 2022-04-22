using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApiTasks.Models;

public class Movie
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
    
    public List<Genre> Genres { get; set; }

    public DateTime ReleaseDate { get; set; }
}

public enum Genre
{
    Action,
    Comedy,
    Drama,
    Fantasy,
    Horror,
    Mystery,
    Romance,
    Thriller,
}