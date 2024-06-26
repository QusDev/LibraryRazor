﻿using LibraryBlazor.Entity.DbContexts;
using LibraryBlazor.Entity.Entities;
using LibraryBlazor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBlazor.Services.Implementation
{
    public class IssueService : IIsueService
    {
        private readonly ILibraryDbContext _libraryDbContext;

        public IssueService(ILibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task AddAsync(Issue entity)
        {
            if (entity is null)
            {
                return;
            }

            _libraryDbContext.Issues.Add(entity);
            await _libraryDbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            var issue = await _libraryDbContext.Issues.FirstOrDefaultAsync(x => x.Id == id);

            if (issue is null)
            {
                return;
            }

            _libraryDbContext.Issues.Remove(issue);
            await _libraryDbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<Issue>> GetAllAsync() => await _libraryDbContext.Issues.Include(i => i.Reader).Include(i => i.Book).ToListAsync();

        public async Task<Issue?> GetAsync(int id) => await _libraryDbContext.Issues.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(int id, Issue entity)
        {
            var issue = await _libraryDbContext.Issues.FirstOrDefaultAsync(x => x.Id == id);

            if (issue is null)
            {
                return;
            }

            issue.IssueDate = entity.IssueDate;
            issue.ReturnDate = entity.ReturnDate;
            issue.Returned = entity.Returned;
            issue.BookId = entity.BookId;
            issue.ReaderId = entity.ReaderId;

            await _libraryDbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
