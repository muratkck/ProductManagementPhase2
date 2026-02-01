using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}