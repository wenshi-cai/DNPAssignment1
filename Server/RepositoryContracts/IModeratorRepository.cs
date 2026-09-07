using Entities;

namespace RepositoryContracts;

public interface IModeratorRepository
{
    Task<Moderator> AddAsync(Moderator moderator);

    Task UpdateAsync(Moderator moderator);

    Task DeleteAsync(int id);

    Task<Moderator> GetSingleAsync(int id);

    IQueryable<Moderator> GetMany();
}