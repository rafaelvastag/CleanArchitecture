using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtos = await _productService.FindAllAsync();
            if (produtos == null)
            {
                return NotFound("Products not found");
            }
            return Ok(produtos);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produto = await _productService.FindByIdAsync(id);
            if (produto == null)
            {
                return NotFound("Product not found");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Data Invalid");
            
            await _productService.AddAsync(produtoDto);

            return new CreatedAtRouteResult("GetProductById",
                new { id = produtoDto.Id }, produtoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO produtoDto)
        {
            if (id != produtoDto.Id)
            {
                return BadRequest("Data invalid");
            }

            if (produtoDto == null)
                return BadRequest("Data invalid");

            await _productService.UpdateAsync(produtoDto);

            return Ok(produtoDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var produtoDto = await _productService.FindByIdAsync(id);

            if (produtoDto == null)
            {
                return NotFound("Product not found");
            }
            var p = new ProductDTO(id);
            await _productService.DeleteAsync(p);

            return Ok(produtoDto);
        }
    }
}
