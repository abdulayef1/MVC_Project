using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel hwm = new HomeViewModel()
            {
                SlideItems=_context.SlideItems.AsNoTracking(),
                ShippingItems=_context.ShippingItems.AsNoTracking()
            };

            return View(hwm);
        } 
        public IActionResult Test()
        {
            return View();
        }
    }
}
