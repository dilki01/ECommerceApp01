using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _service;
        private readonly CategoryService _categoryService;

        public ProductController(ProductService service, CategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        // READ: List all products
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products);
        }

        // CREATE: show form
        public async Task<IActionResult> Create()
        {
            var cats = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(cats, "Id", "Name");

            return View();
        }

        // CREATE: submit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var cats = await _categoryService.GetAllAsync();
                ViewBag.Categories = new SelectList(cats, "Id", "Name", product.CategoryId);
                return View(product);
            }

            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // UPDATE: show edit form
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var cats = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(cats, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // UPDATE: submit edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                var cats = await _categoryService.GetAllAsync();
                ViewBag.Categories = new SelectList(cats, "Id", "Name", product.CategoryId);
                return View(product);
            }

            await _service.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        // DELETE: remove product
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
