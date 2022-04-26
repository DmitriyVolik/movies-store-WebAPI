using System.ComponentModel.DataAnnotations;

namespace BLL.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    
    public Movie Movie { get; set; }

    public Guid? ParentId { get; set; }
    
    public string Username { get; set; }

    public string Body { get; set; }
}