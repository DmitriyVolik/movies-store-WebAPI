using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentDTO
{
    public Guid Id { get; set; }

    [JsonPropertyName("movie_id")]
    public Guid MovieId { get; set; }
    
    [JsonPropertyName("parent_id")]
    public Guid ParentId { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}