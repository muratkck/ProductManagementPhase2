using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}