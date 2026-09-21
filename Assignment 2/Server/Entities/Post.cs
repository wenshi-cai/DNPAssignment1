namespace Entities;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public int UserId { get; set; }

    // 如果实现了 SubForum，一个 Post 属于一个 SubForum
    public int SubForumId { get; set; }
}