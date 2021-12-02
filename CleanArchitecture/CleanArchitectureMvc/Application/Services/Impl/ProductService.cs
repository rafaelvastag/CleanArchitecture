using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ??throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> FindAllAsync()
        {
            var categories = await _productRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(categories);
        }

        public async Task<ProductDTO> FindByIdAsync(int? id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> FindProductWithCategoriesByIdAsync(int? id)
        {
            var product = await _productRepository.FindProductWithCategoriesByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddAsync(ProductDTO obj)
        {
            var entity = _mapper.Map<Product>(obj);
            var product = await _productRepository.CreateAsync(entity);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> DeleteAsync(ProductDTO obj)
        {
            var entity = _mapper.Map<Product>(obj);
            var product = await _productRepository.DeleteAsync(entity);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO obj)
        {
            var entity = _mapper.Map<Product>(obj);
            var product = await _productRepository.UpdateAsync(entity);
            return _mapper.Map<ProductDTO>(product);
        }

    }
}
