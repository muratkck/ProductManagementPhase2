using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);
    }
}