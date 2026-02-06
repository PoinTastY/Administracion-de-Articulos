using Core.Application.DTOs;
using Core.Domain.Enums;

namespace Core.Application.Interfaces
{
    public interface IExtendService
    {
        /// <summary>
        /// Submit a request to extend the deadline of an article
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The PK of the created entity</returns>
        Task<int> CreateExtendRequestAsync(ExtendRequestDto request);
        Task<ICollection<ExtendRequestDto>> GetByStatusAsync(RequestStatus status, int page = 1, int pageSize = 50);
        Task<ExtendRequestDto?> GetLastRequestByStudentCodeAsync(string code);
        Task<ExtendRequestDto?> GetByIdAsync(int id);
    }
}
