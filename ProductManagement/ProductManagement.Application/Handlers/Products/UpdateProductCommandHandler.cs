using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.DTOs.Products;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class UpdateProductCommandHandler(
        IRepository<Product> repository,
        ICacheService cacheService,
        ILogger<UpdateProductCommandHandler> logger) : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating product with ID: {Id}", request.Id);

            var product = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id);
            product.Name = request.ProductDto.Name;
            product.Description = request.ProductDto.Description;
            product.Price = request.ProductDto.Price;
            product.Stock = request.ProductDto.Stock;
            product.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(product);
            await repository.SaveChangesAsync();

            // Invalidate cache
            await cacheService.RemoveByPrefixAsync("products:");

            logger.LogInformation("Product updated successfully");

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