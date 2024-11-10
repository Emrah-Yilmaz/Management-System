using ManagementSystem.Domain.Services.Abstract.Comment;
using MediatR;

public class ChangeStatusCommentCommandHandler : IRequestHandler<ChangeStatusCommentCommand, bool>
{
    private readonly ICommentService _commentService;

    public ChangeStatusCommentCommandHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<bool> Handle(ChangeStatusCommentCommand request, CancellationToken cancellationToken)
    {
        return await _commentService.ChangeStatus(request, cancellationToken);
    }
}