using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid MovieId { get; set; }
    
    [JsonIgnore]
    public Comment? Parent { get; set; }
    
    public Guid? ParentId { get; set; }
    
    [JsonIgnore]
    public ICollection<Comment> SubComments { get; } = new List<Comment>();

    [MaxLength(20)]
    public string Username { get; set; }

    [MaxLength(1000)]
    public string Body { get; set; }
}