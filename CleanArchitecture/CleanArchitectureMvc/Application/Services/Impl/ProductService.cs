using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IEnumerable<ProductDTO>> FindAllAsync()
        {

            var productsQuery = new GetProductsQuery();
            if (null == productsQuery)
                throw new Exception($"Query could not be loaded");

            var products = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> FindByIdAsync(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            var product = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddAsync(ProductDTO obj)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(obj);
            var product = await _mediator.Send(productCreateCommand);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> DeleteAsync(ProductDTO obj)
        {
            var productRemoveCommand = new ProductRemoveCommand(obj.Id);
            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");

            var product = await _mediator.Send(productRemoveCommand);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO obj)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(obj);
            var product = await _mediator.Send(productUpdateCommand);
            return _mapper.Map<ProductDTO>(product);
        }

    }
}
