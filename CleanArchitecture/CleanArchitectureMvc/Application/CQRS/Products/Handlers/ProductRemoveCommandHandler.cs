using Application.CQRS.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {

        private readonly IProductRepository _productRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                    throw new ArgumentNullException(nameof(productRepository));
        }
        
        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            var p = await _productRepository.FindByIdAsync(request.Id);

            if (null == p)
            {
                throw new ApplicationException($"Product not found");
            }
            else
            {
                return await _productRepository.DeleteAsync(p);
            }
        }
    }
}
