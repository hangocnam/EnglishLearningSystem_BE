using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        IQueryable<User> IAppDbContext.Users => Users;
    }
}
