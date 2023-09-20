using AFS.Test.Domain.Entities;

namespace AFS.Test.Application.Contracts.Persistence;

public interface ITranslationCallRepository
{
    Task<IReadOnlyList<TranslationCall>> FilterListAsync(int page, int size, string keyword, string type, DateTime dateAdded);
    Task<int> ListCountAsync();
    Task<TranslationCall?> AddAsync(TranslationCall entity);
}