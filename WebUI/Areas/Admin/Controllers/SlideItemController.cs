using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebUI.Areas.Admin.ViewModels.Slider;
using WebUI.Utilities;
using System.Runtime.InteropServices;
using static NuGet.Packaging.PackagingConstants;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]
public class SlideItemController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    public SlideItemController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        return View(_context.SlideItems);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SlideCreateVM item)
    {
        if (!ModelState.IsValid) return View(item);
        if (item.Phote == null) return BadRequest();
        if (!item.Phote.CheckFileFormat("image/"))
        {
            ModelState.AddModelError("Phote", "File type must be image");
            return View(item);
        }
        if (!item.Phote.CheckFileSize(300))
        {
            ModelState.AddModelError("Phote", "Phote size must be less than 300Kb");
            return View(item);
        }
        string wwwroot = _env.WebRootPath;
        string uniqueName=await item.Phote.CopyFileAsync(wwwroot, "assets", "images", "website-images");

        SlideItem sliteItem = new();
        sliteItem.Title = item.Title;   
        sliteItem.Offer = item.Offer;
        sliteItem.Description = item.Description;
        sliteItem.Phote = uniqueName;
        await _context.AddAsync(sliteItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        if(model.Phote==null) return NotFound();
        string wwwroot = _env.WebRootPath;
        bool status=Helper.DeleteFile(wwwroot, "assets", "images", "website-images",model.Phote);
        //return Content(status.ToString());
        _context.SlideItems.Remove(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }

    public IActionResult Update(int id)
    {
        var model = _context.SlideItems.Find(id);
        if (model == null) return View(model);

        SlideUpdateVM slider = new()
        {

            Title = model.Title,
            Description = model.Description,
            Offer = model.Offer,
            PhotoPath = model.Phote
        };
        return View(slider);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id,SlideUpdateVM item)
    {
        var model = _context.SlideItems.Find(id);
        model.Title = item.Title;
        model.Description=item.Description; 
        model.Offer = item.Offer;
        model.Phote = item.PhotoPath;

        if (model == null) return View(model);

        SlideUpdateVM slider = new()
        {

            Title = model.Title,
            Description = model.Description,
            Offer = model.Offer,
            PhotoPath = model.Phote
        };
        return RedirectToAction(nameof(Index)); 
    }

}
