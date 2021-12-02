using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> FindAllAsync()
        {
            var categories = await _categoryRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> FindByIdAsync(int? id)
        {
            var category = await _categoryRepository.FindByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> AddAsync(CategoryDTO obj)
        {
            var entity = _mapper.Map<Category>(obj);
            var category = await _categoryRepository.CreateAsync(entity);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> DeleteAsync(CategoryDTO obj)
        {
            var entity = _mapper.Map<Category>(obj);
            var category = await _categoryRepository.DeleteAsync(entity);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateAsync(CategoryDTO obj)
        {
            var entity = _mapper.Map<Category>(obj);
            var category = await _categoryRepository.UpdateAsync(entity);
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
