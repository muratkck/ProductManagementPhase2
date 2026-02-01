using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Queries.Products
{
    public class GetProductByIdQuery(int id) : IRequest<ProductDto>
    {
        public int Id { get; set; } = id;
    }
}