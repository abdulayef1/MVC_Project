using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
	public class ProductController : Controller
	{

        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
   
		public IActionResult Index()
		{
            ViewBag.ProductCount=_context.Products.Count();
			var products = _context.Products.Take(8).AsNoTracking();
			return View(products);
		}

        public IActionResult LoadMore(int skip)
        {
            if (skip>=_context.Products.Count())
            {
                return BadRequest();
            }
            var products = _context.Products.Skip(skip).Take(8).AsNoTracking();
            return PartialView("_ProductPartial",products);
        }

    }
}
