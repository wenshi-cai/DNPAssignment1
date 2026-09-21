using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Create New Post ===");

        Console.Write("Enter post title: ");
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Enter post body: ");
        string body = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Body cannot be empty.");
            return;
        }

        Console.Write("Enter user ID: ");
        string userIdInput = Console.ReadLine() ?? "";

        if (!int.TryParse(userIdInput, out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        User? user = await userRepository.GetSingleAsync(userId);

        if (user == null)
        {
            Console.WriteLine("User does not exist.");
            return;
        }

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine();
        Console.WriteLine("Post created successfully!");
        Console.WriteLine($"Post ID: {createdPost.Id}");
        Console.WriteLine($"Title: {createdPost.Title}");
    }
}