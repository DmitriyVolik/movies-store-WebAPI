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
    
    public void AddComment(CommentRequestDTO commentRequest)
    {
        if (commentRequest.ParentId is not null)
        {
            var parentComment = GetCommentById(commentRequest.ParentId);
        
            if (parentComment is null)
            {
                throw new Exception("Incorrect parentId");
            }
            
            commentRequest.Body = commentRequest.Body
                .Insert(0, "[" + parentComment.Username + "]");
        }

        _unitOfWork.Comments.Add(commentRequest);
        _unitOfWork.Save();
    }

    public IEnumerable<CommentResponseDTO> GetCommentsByMovieId(Guid id)
    {
        var comments = _unitOfWork.Comments.GetCommentsByMovieId(id).ToList();
        
        ITree<Comment> virtualRootNode = comments.ToTree((parent, child) => child.ParentId == parent.Id);
        List<ITree<Comment>> flattenedListOfFolderNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
        ITree<Comment> folderNode = flattenedListOfFolderNodes.First();
        TreeManager.GetParents(folderNode);

        return comments.Select(CommentToResponseDto);
    }

    private CommentResponseDTO? GetCommentById(Guid? id)
    {
        var comment = _unitOfWork.Comments.GetById(id);
        return comment is null ? null : CommentToResponseDto(comment);
    }

    public static CommentRequestDTO CommentToRequestDto(Comment comment)
    {
        return new CommentRequestDTO()
        {
            Id = comment.Id,
            ParentId = comment.ParentId,
            MovieId = comment.Movie.Id,
            Username = comment.Username,
            Body = comment.Body
        };
    }
    
    public static CommentResponseDTO CommentToResponseDto(Comment comment)
    {

        var commentResponseDto = new CommentResponseDTO
        {
            Id = comment.Id,
            Username = comment.Username,
            Body = comment.Body
        };

        if (comment.Parent is not null)
        {
            commentResponseDto.ParentComment = CommentToResponseDto(comment.Parent);
        }

        return commentResponseDto;
    }
    
    ~CommentsService()
    {
        _unitOfWork.Dispose();
    }
    
}