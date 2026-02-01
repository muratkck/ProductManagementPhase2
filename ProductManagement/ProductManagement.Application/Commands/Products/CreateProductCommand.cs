using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Commands.Products
{
    public class CreateProductCommand(CreateProductDto productDto) : IRequest<ProductDto>
    {
        public CreateProductDto ProductDto { get; set; } = productDto;
    }
}