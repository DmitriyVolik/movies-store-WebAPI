using BLL.Extensions;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using Models.DTO;

namespace BLL.Services;

public class CommentsService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Comment AddComment(Comment comment)
    {
        if (comment.ParentId is not null)
        {
            var parentComment = _unitOfWork.Comments.GetById(comment.ParentId);
            
            if (parentComment is null) throw new Exception("Incorrect parentId");

            comment.Body = comment.Body
                .Insert(0, "[" + parentComment.Username + "]");
        }

        _unitOfWork.Comments.Add(comment);
        _unitOfWork.Save();
        
        return comment;
    }

    public IEnumerable<CommentTreeDTO> GetCommentsByMovieId(Guid id)
    {
        var comments = _unitOfWork.Comments.GetCommentsByMovieId(id).ToList();

        var virtualRootNode = comments.ToTree((parent, child) => child.ParentId == parent.Id);
        var flattenedListOfNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
        var node = flattenedListOfNodes.First();
        TreeManager.GetParents(node);
        
        return comments.Select(CommentToDTO);
    }

    private Comment? GetCommentById(Guid? id)
    {
        var comment = _unitOfWork.Comments.GetById(id);
        return comment;
    }

    private CommentTreeDTO CommentToDTO(Comment comment)
    {
        var commentResponseDto = new CommentTreeDTO
        {
            Id = comment.Id,
            Username = comment.Username,
            Body = comment.Body
        };

        if (comment.Parent is not null)
        {
            commentResponseDto.ParentCommentTree = CommentToDTO(comment.Parent);
        }

        return commentResponseDto;
    }

    ~CommentsService()
    {
        _unitOfWork.Dispose();
    }
}