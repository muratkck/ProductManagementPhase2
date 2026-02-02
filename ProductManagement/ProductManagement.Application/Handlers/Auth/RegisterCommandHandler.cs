using MediatR;
using ProductManagement.Application.Commands.Auth;
using ProductManagement.Application.DTOs.Auth;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Auth
{
    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterDto;

            // Check if user already exists
            if (await userRepository.UserExistsAsync(dto.Username, dto.Email))
            {
                throw new InvalidOperationException("Username or email already exists");
            }

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Create user
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                FullName = dto.FullName,
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            // Generate JWT token
            var token = jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }
    }
}