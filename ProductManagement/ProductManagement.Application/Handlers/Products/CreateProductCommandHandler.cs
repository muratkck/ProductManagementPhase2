using MediatR;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.DTOs.Products;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class CreateProductCommandHandler(
        IRepository<Product> repository,
        ICacheService cacheService) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.ProductDto.Name,
                Description = request.ProductDto.Description,
                Price = request.ProductDto.Price,
                Stock = request.ProductDto.Stock,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            // Invalidate cache
            await cacheService.RemoveByPrefixAsync("products:");

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}