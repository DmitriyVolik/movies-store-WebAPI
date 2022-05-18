using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid MovieId { get; set; }
    
    [ForeignKey("MovieId")]
    public Movie Movie { get; set; }
    
    [ForeignKey("ParentId")]
    public Comment? Parent { get; set; }
    
    public Guid? ParentId { get; set; }

    [MaxLength(20)]
    public string Username { get; set; }

    [MaxLength(1000)]
    public string Body { get; set; }
}