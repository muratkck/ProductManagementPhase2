using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.DTOs.Products;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class CreateProductCommandHandler(
        IRepository<Product> repository,
        ICacheService cacheService,
        ILogger<CreateProductCommandHandler> logger) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating product with name: {Name}", request.ProductDto.Name);

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

            logger.LogInformation("Product created successfully with ID: {Id}", product.Id);

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