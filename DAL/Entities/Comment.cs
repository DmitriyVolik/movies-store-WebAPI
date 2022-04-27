using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Movie Movie { get; set; }

    public Guid ParentId { get; set; }
    
    public string Username { get; set; }

    public string Body { get; set; }
}