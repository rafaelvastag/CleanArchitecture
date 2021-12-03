using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.FindAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (null == id)
                return NotFound();

            var catOnEdition = await _categoryService.FindByIdAsync(id);

            if (null == catOnEdition)
                return NotFound();

            return View(catOnEdition);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var catEdited = await _categoryService.UpdateAsync(obj);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (null == id)
                return NotFound();

            var catOnEdition = await _categoryService.FindByIdAsync(id);

            if (null == catOnEdition)
                return NotFound();

            return View(catOnEdition);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            var categ = new CategoryDTO(id);
            await _categoryService.DeleteAsync(categ);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (null == id)
                return NotFound();

            var catDetails = await _categoryService.FindByIdAsync(id);

            if (null == catDetails)
                return NotFound();

            return View(catDetails);
        }


    }
}
