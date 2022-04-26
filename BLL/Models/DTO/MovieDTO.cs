using BLL.Models.Enums;

namespace BLL.Models.DTO;

public class MovieDTO
{
    public string Title { get; set; }

    public string Description { get; set; }

    public List<GenreEnum> Genres { get; set; }

    public string Director { get; set; }

    public DateTime ReleaseDate { get; set; }
}

