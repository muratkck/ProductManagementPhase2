using MediatR;
using ProductManagement.Application.DTOs.Auth;

namespace ProductManagement.Application.Commands.Auth
{
    public class LoginCommand(LoginDto loginDto) : IRequest<AuthResponseDto>
    {
        public LoginDto LoginDto { get; set; } = loginDto;
    }
}