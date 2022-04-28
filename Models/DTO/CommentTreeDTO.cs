using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentTreeDTO
{
    public Guid Id { get; set; }
    
    public CommentTreeDTO ParentCommentTree { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}