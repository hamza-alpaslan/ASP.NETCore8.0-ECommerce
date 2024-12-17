using e_commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Components
{
    public class AllProductViewComponent:ViewComponent
    {
        private readonly ApplicationDbcContext dbcContext;
        public AllProductViewComponent(ApplicationDbcContext dbcContext)
        {
            this.dbcContext = dbcContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var products = await dbcContext.products.ToListAsync();
            return View(products);
        }
    }
}
