using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Repository.IRepository;

namespace Repository.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemsController(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            System.Collections.Generic.IEnumerable<Item> items = _itemRepository.GetAll();
            return View(items.ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = await _itemRepository.GetById(id.GetValueOrDefault());
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Create()
        {
            ViewData["Categorylist"] = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Description,Price,StockItem,DateofAddingItem")] Item item)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.Add(item);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categorylist"] = new SelectList(_categoryRepository.GetAll(), "Id", "Name");

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = await _itemRepository.GetById(id.GetValueOrDefault());
            if (item == null)
            {
                return NotFound();
            }
            ViewData["Categorylist"] = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Description,Price,StockItem,DateofAddingItem")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _itemRepository.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    Item itemInDb = await _itemRepository.GetById(id);
                    if (itemInDb == null)
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
            ViewData["Categorylist"] = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(item);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = await _itemRepository.GetById(id.GetValueOrDefault());
            return item == null ? NotFound() : View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Item item = await _itemRepository.GetById(id);
            _itemRepository.Remove(item);
            return RedirectToAction(nameof(Index));
        }
    }
}
