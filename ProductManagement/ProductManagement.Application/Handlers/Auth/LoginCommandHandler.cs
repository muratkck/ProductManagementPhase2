using MediatR;
using ProductManagement.Application.Commands.Auth;
using ProductManagement.Application.DTOs.Auth;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Handlers.Auth
{
    public class LoginCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginDto;

            // Find user
            var user = await userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

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