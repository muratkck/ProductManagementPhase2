using MediatR;

namespace ProductManagement.Application.Commands.Products
{
    public class DeleteProductCommand(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}