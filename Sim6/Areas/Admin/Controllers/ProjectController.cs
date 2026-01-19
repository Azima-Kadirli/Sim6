using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Sim6.Context;
using Sim6.Helper;
using Sim6.Models;
using Sim6.ViewModel.Project;
using System.Threading.Tasks;

namespace Sim6.Areas.Admin.Controllers;
[Area("Admin")]
public class ProjectController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly string _folderPath;

    public ProjectController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
        _folderPath = Path.Combine(_env.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {
        await _sendCategoriesWithVewBag();
        var category = await _context.Projects.Select(p => new ProjectGetVM()
        {
            Id=p.Id,
            CategoryName=p.Category.Name,
            Image=p.Image,
            Title=p.Title
        }).ToListAsync();
        return View(category);
    }

    public async Task<IActionResult> Create()
    {
        await _sendCategoriesWithVewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateVM vm)
    {
        await _sendCategoriesWithVewBag();
        if (!ModelState.IsValid)
            return View(vm);
        var existCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);
        if (!existCategory)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image")??false)
        {
            ModelState.AddModelError("Image", "Zehmet olmasa image tipli sekil yukleyin");
            return View(vm);
        }
        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Seklin max olcusu 2mb ola biler");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);

        Project project = new()
        {
            Title = vm.Title,
            Image=uniqueFileName,
            CategoryId=vm.CategoryId
        };

         _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            return NotFound();
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        await _sendCategoriesWithVewBag();
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            return NotFound();
        ProjectUpdateVM vm = new()
        {
            CategoryId = project.CategoryId,
            Title = project.Title
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProjectUpdateVM vm)
    {
        await _sendCategoriesWithVewBag();
        if (!ModelState.IsValid)
            return View(vm);

        var existCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);
        if (!existCategory)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return View(vm);
        }

        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Sekilin olcusu max 2mb ola biler");
            return View(vm);
        }
        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Zehmet olmasa image tipli sekil yukleyin");
            return View(vm);
        }

        var existProject = await _context.Projects.FindAsync(vm.Id);
        if (existProject is null)
            return NotFound();

        existProject.Title = vm.Title;
        existProject.CategoryId = vm.CategoryId;
        if(vm.Image is not null)
        {
            string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
            string oldImagePath = Path.Combine(_folderPath, existProject.Image);
            ExtensionMethod.DeleteFile(oldImagePath);
            existProject.Image = newImagePath;
        }
        _context.Projects.Update(existProject);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }


    private async Task _sendCategoriesWithVewBag()
    {
        var categories = await _context.Categories.Select(c => new SelectListItem()
        {
            Value = c.Id.ToString(),
            Text=c.Name
        }).ToListAsync();
        ViewBag.Categories = categories;
    }
}
