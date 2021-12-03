using Application.CQRS.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {

        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductCreateCommand request,
            CancellationToken cancellationToken)
        {
            var p = new Product(request.Name, request.Description, request.Price, 
                request.Stock, request.Image);

            if (null == p)
            {
                throw new ApplicationException($"Error while creating entity");
            }
            else
            {
                p.CategoryId = request.CategoryId;
                return await _productRepository.CreateAsync(p);
            }
        }
    }
}
