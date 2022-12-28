using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Repository.IRepository;

namespace Repository.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.IEnumerable<Category> categories = _categoryRepository.GetAll();
            return View(categories.ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category categories = await _categoryRepository.GetById(id.GetValueOrDefault());
            return categories == null ? NotFound() : View(categories);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category categories)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(categories);
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category categories = await _categoryRepository.GetById(id.GetValueOrDefault());
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Update(categories);
                }
                catch (DbUpdateConcurrencyException)
                {
                    Category categoryDb = await _categoryRepository.GetById(id);
                    if (categoryDb == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category categories = await _categoryRepository.GetById(id.GetValueOrDefault());
            return categories == null ? NotFound() : View(categories);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category categories = await _categoryRepository.GetById(id);
            _categoryRepository.Remove(categories);
            return RedirectToAction(nameof(Index));
        }
    }
}
