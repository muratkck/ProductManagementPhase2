using MediatR;
using ProductManagement.Application.Commands.Products;
using ProductManagement.Application.Handlers.Products;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Handlers.Products
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IRepository<Product> _repository;
        private readonly ICacheService _cacheService;

        public DeleteProductCommandHandler(
            IRepository<Product> repository,
            ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id {request.Id} not found");
            }

            var result = await _repository.DeleteAsync(request.Id);

            if (result)
            {
                await _repository.SaveChangesAsync();

                // Invalidate cache
                await _cacheService.RemoveByPrefixAsync("products:");
            }

            return result;
        }
    }
}