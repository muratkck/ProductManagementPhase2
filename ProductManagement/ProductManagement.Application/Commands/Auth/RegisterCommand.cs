using MediatR;
using ProductManagement.Application.DTOs.Auth;

namespace ProductManagement.Application.Commands.Auth
{
    public class RegisterCommand(RegisterDto registerDto) : IRequest<AuthResponseDto>
    {
        public RegisterDto RegisterDto { get; set; } = registerDto;
    }
}