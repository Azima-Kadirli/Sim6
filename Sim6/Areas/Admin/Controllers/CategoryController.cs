using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim6.Context;
using Sim6.Models;
using Sim6.ViewModel.Category;
using System.Threading.Tasks;

namespace Sim6.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var category = await _context.Categories.Select(c => new CategoryGetVM()
        {
            Id = c.Id,
            Name = c.Name
        }).ToListAsync();

        return View(category);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        Category category = new()
        {
            Name = vm.Name
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if(category is null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return NotFound();
        CategoryUpdateVM vm = new()
        {
            Name = category.Name
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryUpdateVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var category = await _context.Categories.FindAsync(vm.Id);
        if (category is null)
            return NotFound();
        category.Name = vm.Name;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
