using Entities;
using RepositoryContracts;

namespace InMemoryRepositories_;

public class ModeratorInMemoryRepository : IModeratorRepository
{
    private readonly List<Moderator> moderators = [];
    
    public ModeratorInMemoryRepository()
    {
        moderators.Add(new Moderator
        {
            Id = 1,
            UserId = 1,
            SubForumId = 1
        });

        moderators.Add(new Moderator
        {
            Id = 2,
            UserId = 2,
            SubForumId = 2
        });

        moderators.Add(new Moderator
        {
            Id = 3,
            UserId = 3,
            SubForumId = 3
        });
    }

    public Task<Moderator> AddAsync(Moderator moderator)
    {
        moderator.Id = moderators.Any()
            ? moderators.Max(m => m.Id) + 1
            : 1;

        moderators.Add(moderator);
        return Task.FromResult(moderator);
    }

    public Task UpdateAsync(Moderator moderator)
    {
        Moderator? existing = moderators.SingleOrDefault(m => m.Id == moderator.Id);

        if (existing is null)
            throw new InvalidOperationException($"Moderator with ID '{moderator.Id}' not found");

        moderators.Remove(existing);
        moderators.Add(moderator);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Moderator? moderator = moderators.SingleOrDefault(m => m.Id == id);

        if (moderator is null)
            throw new InvalidOperationException($"Moderator with ID '{id}' not found");

        moderators.Remove(moderator);

        return Task.CompletedTask;
    }

    public Task<Moderator> GetSingleAsync(int id)
    {
        Moderator? moderator = moderators.SingleOrDefault(m => m.Id == id);

        if (moderator is null)
            throw new InvalidOperationException($"Moderator with ID '{id}' not found");

        return Task.FromResult(moderator);
    }

    public IQueryable<Moderator> GetMany()
    {
        return moderators.AsQueryable();
    }
}