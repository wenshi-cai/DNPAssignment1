using Entities;
using RepositoryContracts;

namespace InMemoryRepositories_;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = [];
    public CommentInMemoryRepository()
    {
        comments.Add(new Comment
        {
            Id = 1,
            Body = "This is a very interesting post.",
            UserId = 2,
            PostId = 1
        });

        comments.Add(new Comment
        {
            Id = 2,
            Body = "I agree with your opinion.",
            UserId = 3,
            PostId = 1
        });

        comments.Add(new Comment
        {
            Id = 3,
            Body = "Thank you for sharing this information.",
            UserId = 1,
            PostId = 2
        });
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;

        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);

        if (existingComment is null)
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");

        comments.Remove(existingComment);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);

        if (comment is null)
            throw new InvalidOperationException($"Comment with ID '{id}' not found");

        comments.Remove(comment);

        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);

        if (comment is null)
            throw new InvalidOperationException($"Comment with ID '{id}' not found");

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}