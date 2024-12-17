using e_commerce.Models.Entities;

namespace e_commerce.Models
{
    [Serializable]
    public class Cart
    {
        private List<CartLine> cartLines = new List<CartLine>();

        public List<CartLine> CartLines { get {  return cartLines; } }
        public float TotalCart { get { return calculate_total_price(); } }
        public void Clear()
        {
            cartLines.Clear();
        }
        public void AddCart(Product product,int quantity)
        {
            var line = cartLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (line == null) {
                cartLines.Add(new CartLine() { Product = product, quantity = quantity, total_product_price=product.Price*quantity });
            }
            else
            {
                line.quantity += quantity;
                line.total_product_price = product.Price*line.quantity;
            }
        }
        public float calculate_total_price()
        {
            float totalcart = 0;
            if(cartLines == null)
            {
                return 0;
            }
            foreach (var line in cartLines)
            {
                totalcart += line.total_product_price;
            }
            return totalcart;
        }
        public void Delete(Product product)
        {
            var line = cartLines.FirstOrDefault(i=>i.Product.Id == product.Id);
            if(line == null)
            {
                return;
            }
            else
            {
                cartLines.Remove(line);
                if(cartLines.Count == 0)
                {
                    cartLines = null;
                }
            }
        }
    }
    [Serializable]
    public class CartLine
    {
        public Product Product { get; set; }
        public int quantity { get; set; }
        public float total_product_price { set; get; }
    }
}
