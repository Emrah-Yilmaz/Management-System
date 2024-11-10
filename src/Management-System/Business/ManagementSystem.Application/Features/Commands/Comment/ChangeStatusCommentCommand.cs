using MediatR;

public class ChangeStatusCommentCommand :ChangeStatusCommentArgs,  IRequest<bool>
{
}