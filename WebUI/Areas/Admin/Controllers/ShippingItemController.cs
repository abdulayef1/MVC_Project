﻿using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ShippingItemController : Controller
    {
        private readonly AppDbContext _context;
        public ShippingItemController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ShippingItems);
        }
        public async Task <IActionResult> Detail(int id)
        {
            var model = await _context.ShippingItems.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingItem item)
        {
            if (!ModelState.IsValid) return View(item);
            await _context.ShippingItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task <IActionResult> Update(int id)
        {
            var model=await _context.ShippingItems.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,ShippingItem item)
        {
            if (id != item.Id) return BadRequest();
            if (!ModelState.IsValid) return View(item);
            var model = await _context.ShippingItems.FindAsync(id);
            if (model == null) return NotFound();
            model.Tittle = item.Tittle;
            model.Description = item.Description;
            model.Photo = item.Photo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.ShippingItems.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeletePost(int id)
        {
            var model = await _context.ShippingItems.FindAsync(id);
            if (model == null) return NotFound();
            _context.ShippingItems.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
