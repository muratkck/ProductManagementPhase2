using MediatR;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class DeleteProductCommandHandler(
        IRepository<Product> repository,
        ICacheService cacheService) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Product with id {request.Id} not found");
            var result = await repository.DeleteAsync(request.Id);

            if (result)
            {
                await repository.SaveChangesAsync();

                await cacheService.RemoveByPrefixAsync("products:");
            }

            return result;
        }
    }
}