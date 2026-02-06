using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Domain.Interfaces
{
    public interface IExtendRepo
    {
        Task<IQueryable<ExtendRequest>> GetByStatusAsync(RequestStatus status, int page = 1, int pageTop = 10);
        Task<ICollection<ExtendRequest>> GetByStudentCodeAsync(string code, int page = 1, int pageTop = 10);
        Task<ExtendRequest?> GetByIdAsync(int id);
        Task<ExtendRequest> AddAsync(ExtendRequest entity);
        Task<ExtendRequest> UpdateAsync(ExtendRequest entity);
    }
}
