using Entities;
using RepositoryContracts;

namespace InMemoryRepositories_;

public class SubForumInMemoryRepository : ISubForumRepository
{
    private readonly List<SubForum> subForums = [];

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