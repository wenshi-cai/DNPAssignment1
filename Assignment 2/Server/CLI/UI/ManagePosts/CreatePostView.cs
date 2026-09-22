using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task StartAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=== Create New Post ===");

        Console.Write("Enter title: ");
        string title = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Enter body: ");
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

        Console.Write("Enter subforum ID: ");
        string subForumIdInput = Console.ReadLine() ?? "";

        if (!int.TryParse(subForumIdInput, out int subForumId))
        {
            Console.WriteLine("Invalid subforum ID.");
            return;
        }

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
            SubForumId = subForumId
        };

        Post createdPost = await postRepository.AddAsync(post);

        Console.WriteLine();
        Console.WriteLine("Post created successfully!");
        Console.WriteLine($"Post ID: {createdPost.Id}");
        Console.WriteLine($"Title: {createdPost.Title}");
    }
}