using Application.CQRS.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {

        private readonly IProductRepository _productRepository;

        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductUpdateCommand request,
            CancellationToken cancellationToken)
        {
            var p = await _productRepository.FindByIdAsync(request.Id);

            if (null == p)
            {
                throw new ApplicationException($"Product not found");
            }
            else
            {
                p.Update(request.Name, request.Description, request.Price,
                request.Stock, request.Image, request.CategoryId);

                return await _productRepository.UpdateAsync(p);
            }
        }
    }
}
