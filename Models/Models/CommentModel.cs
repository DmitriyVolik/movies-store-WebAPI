
namespace Models.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    
    public CommentModel? ParentComment { get; set; }

    public string Username { get; set; }

    public string Body { get; set; }
}