using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRefreshToken
    {
        Task<RefreshToken?> GetByTokenAsync(string token);

        Task AddAsync(string refreshToken, Guid Id);

        Task UpdateAsync(Domain.Models.RefreshToken refreshToken, string token);
    }
}