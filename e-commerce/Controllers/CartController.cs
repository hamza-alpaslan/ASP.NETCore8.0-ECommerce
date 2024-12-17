using e_commerce.Data;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace e_commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbcContext dbcContext;
        private readonly ILogger<CartController> logger;

        public CartController(ApplicationDbcContext dbcContext, ILogger<CartController> logger)
        {
            this.dbcContext = dbcContext;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var cart = GetCart();
                if (cart == null)
                {
                    logger.LogWarning("Cart is null. Initializing a new cart.");
                    cart = new Cart();
                }
                logger.LogInformation("Cart retrieved successfully with {ItemCount} items.", cart.CartLines.Count);
                return View(cart);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the cart.");
                return View(new Cart());
            }
        }

        [HttpPost]
        public IActionResult AddCart(Guid id, int quantity)
        {
            try
            {
                var product = dbcContext.products.FirstOrDefault(i => i.Id == id);
                if (product == null)
                {
                    logger.LogWarning("Product with ID {ProductId} not found.", id);
                    return RedirectToAction("Index");
                }

                var cart = GetCart();
                if (quantity <= 0)
                {
                    logger.LogWarning("Invalid quantity: {Quantity}. Defaulting to 1.", quantity);
                    quantity = 1; 
                }

                cart.AddCart(product, quantity);
                SaveCart(cart);

                logger.LogInformation("Product {ProductName} with quantity {Quantity} added to cart successfully.", product.Name, quantity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a product to the cart.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(Guid id)
        {
            var product = dbcContext.products.FirstOrDefault(i=>i.Id == id);
            if (product == null)
            {
                return View();
            }
            var cart = GetCart();
            cart.Delete(product);
            SaveCart(cart);
            return RedirectToAction("Index");
        }
        private Cart GetCart()
        {
            try
            {
                var cartJson = HttpContext.Session.GetString("Cart");
                if (string.IsNullOrEmpty(cartJson))
                {
                    logger.LogWarning("No cart data found in session.");
                    return new Cart();
                }

                logger.LogInformation("Cart data retrieved from session.");
                return JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deserializing the cart.");
                return new Cart();
            }
        }

        private void SaveCart(Cart cart)
        {
            try
            {
                var cartJson = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString("Cart", cartJson);
                logger.LogInformation("Cart data saved to session successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while saving the cart to session.");
            }
        }
    }
}
