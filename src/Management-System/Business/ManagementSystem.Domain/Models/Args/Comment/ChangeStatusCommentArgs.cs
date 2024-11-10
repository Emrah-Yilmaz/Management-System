using ManagementSystem.Domain.Models.Enums;

public class ChangeStatusCommentArgs{
    public int CommentId { get; set; }
    public StatusType StatusType { get; set; }
}