using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Domain.Entities;
using AFS.Test.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AFS.Test.Persistence.Repositories;

public class TranslationCallRepository : ITranslationCallRepository
{
    private readonly TranslationDbContext _dbContext;

    public TranslationCallRepository(TranslationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyList<TranslationCall>> FilterListAsync(int page, int size, string keyword, string type, DateTime dateAdded)
    {
        var listToReturn = _dbContext.TranslationCalls.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
            listToReturn = listToReturn.Where(l => l.Text.ToLower().Contains(keyword.ToLower()) || l.User.ToLower().Contains(keyword.ToLower()));

        if (!string.IsNullOrWhiteSpace(type))
            listToReturn = listToReturn.Where(l => l.Translation.ToLower() == type.ToLower());

        if (dateAdded != new DateTime())
        {
            listToReturn = listToReturn.Where(l => DateTime.Equals(l.TimeStamp.Date, dateAdded.Date) );
        }
        
        listToReturn = listToReturn
            .Skip((page - 1) * size).Take(size)
            .AsNoTracking()
            .OrderByDescending(l => l.TimeStamp);

        return await listToReturn.ToListAsync();
    }

    public async Task<int> ListCountAsync()
    {
        var count =  await _dbContext.TranslationCalls.CountAsync();
        return count;
    }

    public async Task<TranslationCall?> AddAsync(TranslationCall entity)
    {
        _dbContext.TranslationCalls.Add(entity);
        var result = await _dbContext.SaveChangesAsync();

        if (result == 1)
        {
            return entity;
        }

        return null;
    }
}