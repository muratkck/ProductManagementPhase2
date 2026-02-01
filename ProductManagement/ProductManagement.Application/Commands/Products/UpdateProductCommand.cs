using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Commands.Products
{
    public class UpdateProductCommand(int id, UpdateProductDto productDto) : IRequest<ProductDto>
    {
        public int Id { get; set; } = id;
        public UpdateProductDto ProductDto { get; set; } = productDto;
    }
}