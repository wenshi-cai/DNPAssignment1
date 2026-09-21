namespace Entities;

public class SubForum
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    // 创建该 SubForum 的用户
    public int UserId { get; set; }
}