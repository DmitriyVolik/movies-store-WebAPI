using System.Text.Json.Serialization;

namespace Models.DTO;

public class CommentResponseDTO
{
    public Guid Id { get; set; }
    
    public CommentResponseDTO ParentComment { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}