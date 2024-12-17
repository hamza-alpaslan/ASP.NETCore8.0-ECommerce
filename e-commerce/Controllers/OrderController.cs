using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace e_commerce.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View(GetOrder());
        }
        public IActionResult AddToOrder()
        {
            var json_cart = HttpContext.Session.GetString("Cart");
            var cart= JsonConvert.DeserializeObject<Cart>(json_cart);

            var order = GetOrder();

            order.AddOrder(cart);
            SaveOrder(order);
            cart.Clear();
            json_cart=JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart",json_cart);
            return RedirectToAction("Index");

        }
        public IActionResult Delete(Guid  id) {

            var order = GetOrder();
            order.Delete(id);
            SaveOrder(order);
            return RedirectToAction("Index");
        }
        public void SaveOrder(Order order)
        {
            var OrderJson=JsonConvert.SerializeObject(order);
            HttpContext.Session.SetString("Order",OrderJson);

        }
        public Order GetOrder()
        {
            var Orderjson = HttpContext.Session.GetString("Order");
            if (string.IsNullOrEmpty(Orderjson))
            {
                return new Order();
            }
            return JsonConvert.DeserializeObject<Order>(Orderjson);
        }
    }
}
