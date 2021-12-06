using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> FindAll()
        {
            var categories = await _categoryService.FindAllAsync();

            if (null == categories)
                return NotFound("Categories not found");

            return Ok(categories);
        }


        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> FindById(int id)
        {
            var category = await _categoryService.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO category)
        {
            if (category == null)
                return BadRequest("Invalid Data");

            await _categoryService.AddAsync(category);

            return new CreatedAtRouteResult("GetCategoryById", new { id = category.Id}, category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest();

            if (categoryDto == null)
                return BadRequest();

            await _categoryService.UpdateAsync(categoryDto);

            return Ok(categoryDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _categoryService.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            var cat = new CategoryDTO(id);
            await _categoryService.DeleteAsync(cat);

            return Ok(category);

        }
    }
}
