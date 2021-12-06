using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService, ICategoryService categoryService,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.FindAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.FindAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO obj)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(obj);
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (null == id)
                return NotFound();
            var product = await _productService.FindByIdAsync(id);

            if (null == product)
                return NotFound();

            var categories = await _categoryService.FindAllAsync();

            ViewBag.CategoryId = new SelectList(categories,"Id", "Name",product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(p);
                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (null == id)
                return NotFound();
            var product = await _productService.FindByIdAsync(id);

            if (null == product)
                return NotFound();

            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot,"images", product.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (null == id)
                return NotFound();
            var product = await _productService.FindByIdAsync(id);

            if (null == product)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ProductDTO p = new ProductDTO(id);
            await _productService.DeleteAsync(p);
            return RedirectToAction(nameof(Index));
        }
    }
}
