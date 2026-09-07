namespace Entities;

public class Moderator
{
    public int Id { get; set; }

    // Moderator 对应的用户
    public int UserId { get; set; }

    // 管理的 SubForum
    public int SubForumId { get; set; }
}