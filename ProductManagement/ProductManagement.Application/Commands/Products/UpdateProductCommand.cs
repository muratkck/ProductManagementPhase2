using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
        public UpdateProductDto ProductDto { get; set; }

        public UpdateProductCommand(int id, UpdateProductDto productDto)
        {
            Id = id;
            ProductDto = productDto;
        }
    }
}