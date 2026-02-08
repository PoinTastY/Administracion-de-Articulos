using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repos;

public class ExtendRepo : IExtendRepo
{
    private readonly ArticleManagementDbContext _context;
    private readonly ILogger<ExtendRepo> _logger;

    public ExtendRepo(ArticleManagementDbContext context, ILogger<ExtendRepo> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IQueryable<ExtendRequest>> GetByStatusAsync(RequestStatus status, int page = 1, int pageTop = 10)
    {
        return _context.ExtendRequests.Where(er => er.Status == status)
            .OrderByDescending(er => er.CreatedAt)
            .Skip((page - 1) * pageTop)
            .Take(pageTop)
            .AsQueryable();
    }
    public async Task<ICollection<ExtendRequest>> GetByStudentCodeAsync(string code, int page = 1, int pageTop = 10)
    {
        return await _context.ExtendRequests
            .Where(er => er.StudentCode == code)
            .OrderByDescending(er => er.CreatedAt)
            .Skip((page - 1) * pageTop)
            .Take(pageTop)
            .ToListAsync();
    }

    public async Task<ExtendRequest?> GetByIdAsync(int id)
    {
        return await _context.ExtendRequests.FindAsync(id);
    }
    public async Task<ExtendRequest> AddAsync(ExtendRequest entity)
    {
        _context.ExtendRequests.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<ExtendRequest> UpdateAsync(ExtendRequest entity)
    {
        _context.ExtendRequests.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
