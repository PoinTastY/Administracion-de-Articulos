using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Domain.Interfaces
{
    public interface IExtendRepo
    {
        Task<IQueryable<ExtendRequest>> GetByStatusAsync(RequestStatus status, int page, int pageTop);
        Task<ICollection<ExtendRequest>> GetByStudentCodeAsync(string code);
        Task<ICollection<ExtendRequest>> GetByEmailAsync(string code);
        Task<ExtendRequest?> GetByIdAsync(int id);
        Task<ExtendRequest> AddAsync(ExtendRequest entity);
        Task<ExtendRequest> UpdateAsync(ExtendRequest entity);
    }
}
