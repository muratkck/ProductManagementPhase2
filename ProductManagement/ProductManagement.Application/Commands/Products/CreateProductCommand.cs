using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public CreateProductDto ProductDto { get; set; }

        public CreateProductCommand(CreateProductDto productDto)
        {
            ProductDto = productDto;
        }
    }
}