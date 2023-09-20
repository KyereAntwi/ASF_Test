using AFS.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFS.Test.Persistence.Data;

public class TranslationDbContext : DbContext
{
    public TranslationDbContext(DbContextOptions<TranslationDbContext> options) : base(options)
    {
    }

    public DbSet<TranslationCall> TranslationCalls { get; set; } = default!;
}