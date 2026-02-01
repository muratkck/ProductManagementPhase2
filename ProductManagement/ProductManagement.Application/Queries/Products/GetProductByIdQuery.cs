using MediatR;
using ProductManagement.Application.DTOs.Products;

namespace ProductManagement.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}