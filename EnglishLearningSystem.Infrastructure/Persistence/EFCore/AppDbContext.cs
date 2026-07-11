using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Word> Words => Set<Word>();
        public DbSet<WordFamily> WordFamilies => Set<WordFamily>();
        public DbSet<ContextExample> ContextExamples => Set<ContextExample>();

    }
}