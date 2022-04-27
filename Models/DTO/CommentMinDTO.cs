using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentMinDTO
{
    public Guid Id { get; set; }

    [JsonPropertyName("parent_comment")]
    public CommentMinDTO ParentComment { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}