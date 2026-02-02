using MediatR;
using ProductManagement.Application.DTOs.Products;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.Products;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class GetProductByIdQueryHandler(
        IRepository<Product> repository,
        ICacheService cacheService) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"products:{request.Id}";

            // Try to get from cache
            var cachedProduct = await cacheService.GetAsync<ProductDto>(cacheKey);
            if (cachedProduct != null)
            {
                return cachedProduct;
            }

            // Get from database
            var product = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id);
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };

            // Cache for 5 minutes
            await cacheService.SetAsync(cacheKey, productDto, TimeSpan.FromMinutes(5));

            return productDto;
        }
    }
}