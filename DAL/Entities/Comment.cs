using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Movie Movie { get; set; }

    public Comment Parent { get; set; }
    
    public Guid? ParentId { get; set; }
    
    public ICollection<Comment> SubComments { get; } = new List<Comment>();

    public string Username { get; set; }

    public string Body { get; set; }
}