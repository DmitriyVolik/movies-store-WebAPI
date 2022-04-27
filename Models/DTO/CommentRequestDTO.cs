using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentRequestDTO
{
    public Guid Id { get; set; }
    
    public Guid MovieId { get; set; }
    
    public Guid? ParentId { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}