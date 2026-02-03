using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class DeleteProductCommandHandler(
        IRepository<Product> repository,
        ICacheService cacheService,
        ILogger<DeleteProductCommandHandler> logger) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting product with ID: {Id}", request.Id);

            var product = await repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id);
            var result = await repository.DeleteAsync(request.Id);

            if (result)
            {
                await repository.SaveChangesAsync();

                await cacheService.RemoveByPrefixAsync("products:");
                logger.LogInformation("Product deleted successfully");
            }

            return result;
        }
    }
}