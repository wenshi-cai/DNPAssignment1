using Entities;
using RepositoryContracts;

namespace InMemoryRepositories_;

public class SubForumInMemoryRepository : ISubForumRepository
{
    private readonly List<SubForum> subForums = [];
    public SubForumInMemoryRepository()
    {
        subForums.Add(new SubForum
        {
            Id = 1,
            Name = "General Discussion",
            Description = "A place for general discussions.",
            UserId = 1
        });

        subForums.Add(new SubForum
        {
            Id = 2,
            Name = "Programming",
            Description = "Discuss programming and software development.",
            UserId = 2
        });

        subForums.Add(new SubForum
        {
            Id = 3,
            Name = "Announcements",
            Description = "Important announcements and updates.",
            UserId = 1
        });
    }

    public Task<SubForum> AddAsync(SubForum subForum)
    {
        subForum.Id = subForums.Any()
            ? subForums.Max(s => s.Id) + 1
            : 1;

        subForums.Add(subForum);
        return Task.FromResult(subForum);
    }

    public Task UpdateAsync(SubForum subForum)
    {
        SubForum? existing = subForums.SingleOrDefault(s => s.Id == subForum.Id);

        if (existing is null)
            throw new InvalidOperationException($"SubForum with ID '{subForum.Id}' not found");

        subForums.Remove(existing);
        subForums.Add(subForum);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        SubForum? subForum = subForums.SingleOrDefault(s => s.Id == id);

        if (subForum is null)
            throw new InvalidOperationException($"SubForum with ID '{id}' not found");

        subForums.Remove(subForum);

        return Task.CompletedTask;
    }

    public Task<SubForum> GetSingleAsync(int id)
    {
        SubForum? subForum = subForums.SingleOrDefault(s => s.Id == id);

        if (subForum is null)
            throw new InvalidOperationException($"SubForum with ID '{id}' not found");

        return Task.FromResult(subForum);
    }

    public IQueryable<SubForum> GetMany()
    {
        return subForums.AsQueryable();
    }
}