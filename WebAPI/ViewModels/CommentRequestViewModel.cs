using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels;

public class CommentRequestViewModel
{
    public Guid MovieId { get; set; }

    public Guid? ParentId { get; set; }

    [MaxLength(20)]
    public string Username { get; set; }

    [MaxLength(1000)]
    public string Body { get; set; }
}