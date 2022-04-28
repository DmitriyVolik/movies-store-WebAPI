using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentTreeDTO
{
    public Guid Id { get; set; }
    
    [JsonPropertyName("parent_comment")]
    public CommentTreeDTO ParentCommentTree { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}