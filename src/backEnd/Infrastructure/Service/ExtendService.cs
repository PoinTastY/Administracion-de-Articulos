using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Service;

public class ExtendService : IExtendService
{
    private readonly IExtendRepo _extendRepo;
    public ExtendService(IExtendRepo extendRepo)
    {
        _extendRepo = extendRepo;
    }

    public async Task<int> CreateExtendRequestAsync(ExtendRequestDto request)
    {
        ExtendRequest entity = new()
        {
            StudentCode = request.StudentCode,
            Article = (Article)request.Article,
            EvidenceFileUrl = request.EvidenceFileUrl,
            Justification = request.Reason,
            Status = RequestStatus.Submitted,
            CreatedAt = DateTime.UtcNow
        };

        ExtendRequest createdEntity = await _extendRepo.AddAsync(entity);

        return createdEntity.Id;
    }

    public async Task<ICollection<ExtendRequestDto>> GetByStatusAsync(RequestStatus status, int page = 1, int pageSize = 50)
    {
        List<ExtendRequest> extendRequests = await (await _extendRepo.GetByStatusAsync(status, page, pageSize)).ToListAsync();

        return [ .. extendRequests.Select(er => new ExtendRequestDto
        {
            StudentCode = er.StudentCode,
            Article = (int)er.Article,
            EvidenceFileUrl = er.EvidenceFileUrl,
            Reason = er.Justification,
            Status = er.Status.GetName(),
            CreatedAt = er.CreatedAt
        })];
    }

    public async Task<ExtendRequestDto?> GetLastRequestByStudentCodeAsync(string code)
    {
        var requests = await _extendRepo.GetByStudentCodeAsync(code, page: 1, pageTop: 1);
        var lastRequest = requests.OrderByDescending(r => r.CreatedAt).FirstOrDefault();

        if (lastRequest == null)
        {
            return null;
        }

        return new ExtendRequestDto
        {
            StudentCode = lastRequest.StudentCode,
            Article = (int)lastRequest.Article,
            EvidenceFileUrl = lastRequest.EvidenceFileUrl,
            Reason = lastRequest.Justification,
            Status = lastRequest.Status.GetName(),
            CreatedAt = lastRequest.CreatedAt
        };
    }

    public async Task<ExtendRequestDto?> GetByIdAsync(int id)
    {
        var request = await _extendRepo.GetByIdAsync(id);
        if (request == null)
        {
            return null;
        }

        return new ExtendRequestDto
        {
            StudentCode = request.StudentCode,
            Article = (int)request.Article,
            EvidenceFileUrl = request.EvidenceFileUrl,
            Reason = request.Justification,
            Status = request.Status.GetName(),
            CreatedAt = request.CreatedAt
        };
    }
}
