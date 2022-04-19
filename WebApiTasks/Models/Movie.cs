namespace WebApiTasks.Models;

public class Movie
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<string> Genres { get; set; }

    public DateTime ReleaseDate { get; set; }
}