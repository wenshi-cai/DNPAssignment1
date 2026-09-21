using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class AddCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public AddCommentView(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Add Comment ===");

        Console.Write("Enter post ID: ");
        string postInput = Console.ReadLine() ?? "";

        if (!int.TryParse(postInput, out int postId))
        {
            Console.WriteLine("Invalid post ID.");
            return;
        }

        try
        {
            await postRepository.GetSingleAsync(postId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.Write("Enter user ID: ");
        string userInput = Console.ReadLine() ?? "";

        if (!int.TryParse(userInput, out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        try
        {
            await userRepository.GetSingleAsync(userId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.Write("Enter comment body: ");
        string body = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Comment cannot be empty.");
            return;
        }

        Comment comment = new Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };

        Comment createdComment = await commentRepository.AddAsync(comment);

        Console.WriteLine();
        Console.WriteLine("Comment added successfully!");
        Console.WriteLine($"Comment ID: {createdComment.Id}");
    }
}