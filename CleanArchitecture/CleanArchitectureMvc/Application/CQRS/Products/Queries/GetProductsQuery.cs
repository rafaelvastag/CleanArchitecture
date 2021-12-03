using Domain.Entities;
using MediatR;
using System.Collections.Generic;
  
namespace Application.CQRS.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
