using Models.Enums;

namespace Models.DTO;

public class MovieModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public List<GenreEnum> Genres { get; set; }

    public string Director { get; set; }
    
    public DateTime ReleaseDate { get; set; }
}

