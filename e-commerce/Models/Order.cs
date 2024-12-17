using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace e_commerce.Models
{
    public class Order
    {
        private List<OrderLine> _lines = new List<OrderLine>();
        public List<OrderLine> Lines { get {  return _lines; } }

        public void AddOrder(Cart cart)
        {
            if (cart == null) throw new ArgumentNullException("Chart is empty");
            else
            {
                var line = new OrderLine();
                line.Cart = cart;
                line.Id=Guid.NewGuid();
                _lines.Add(line);
            }
        }

        public void Delete(Guid Id)
        {
            var line=_lines.FirstOrDefault(i=>i.Id == Id);
            if (line == null)
            {
                throw new ArgumentNullException("Id cant find");
            }
            else
            {
                _lines.Remove(line);
            }
        }
    }
    [Serializable]
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Cart Cart { get; set; }
    }
}
