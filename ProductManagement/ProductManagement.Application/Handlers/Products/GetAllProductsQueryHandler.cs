using MediatR;
using ProductManagement.Application.DTOs.Products;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.Products;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class GetAllProductsQueryHandler(
        IRepository<Product> repository,
        ICacheService cacheService) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "products:all";

            // Try to get from cache
            var cachedProducts = await cacheService.GetAsync<IEnumerable<ProductDto>>(cacheKey);
            if (cachedProducts != null)
            {
                return cachedProducts;
            }

            // Get from database
            var products = await repository.GetAllAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            // Cache for 5 minutes
            await cacheService.SetAsync(cacheKey, productDtos, TimeSpan.FromMinutes(5));

            return productDtos;
        }
    }
}