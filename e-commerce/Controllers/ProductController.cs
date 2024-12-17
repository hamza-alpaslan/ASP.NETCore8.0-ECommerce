using e_commerce.Data;
using e_commerce.Models;
using e_commerce.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbcContext dbcContext;
        public ProductController(ApplicationDbcContext dbcContext)
        {
            this.dbcContext = dbcContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await dbcContext.products.ToListAsync();
            return View(products);
        }

        public IActionResult Details(Guid Id)
        {
            var product= dbcContext.products.FirstOrDefault(x => x.Id == Id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> Category()
        {
            string id = Request.Query["id"];
            var products = await dbcContext.products
                                .Where(p => p.category == id) 
                                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}
