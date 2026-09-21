using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== View Specific Post ===");

        Console.Write("Enter post ID: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        Post post = await postRepository.GetSingleAsync(postId);

        if (post == null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine($"User ID: {post.UserId}");

        Console.WriteLine();
        Console.WriteLine("=== Comments ===");

        var comments = commentRepository
            .GetMany()
            .Where(comment => comment.PostId == postId)
            .ToList();

        if (!comments.Any())
        {
            Console.WriteLine("No comments found.");
            return;
        }

        foreach (Comment comment in comments)
        {
            Console.WriteLine();
            Console.WriteLine($"Comment ID: {comment.Id}");
            Console.WriteLine($"User ID: {comment.UserId}");
            Console.WriteLine($"Body: {comment.Body}");
        }
    }
}