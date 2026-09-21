using Entities;
using RepositoryContracts;

namespace InMemoryRepositories_;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = [];
    public PostInMemoryRepository()
    {
        posts.Add(new Post
        {
            Id = 1,
            Title = "Welcome to the Forum",
            Body = "This is the first test post.",
            UserId = 1,
            SubForumId = 1
        });

        posts.Add(new Post
        {
            Id = 2,
            Title = "Learning C#",
            Body = "I am learning C# and .NET programming.",
            UserId = 2,
            SubForumId = 1
        });

        posts.Add(new Post
        {
            Id = 3,
            Title = "Assignment 2",
            Body = "This post is about the CLI assignment.",
            UserId = 1,
            SubForumId = 2
        });
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;

        posts.Add(post);

        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost is null)
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);

        if (postToRemove is null)
            throw new InvalidOperationException($"Post with ID '{id}' not found");

        posts.Remove(postToRemove);

        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);

        if (post is null)
            throw new InvalidOperationException($"Post with ID '{id}' not found");

        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}