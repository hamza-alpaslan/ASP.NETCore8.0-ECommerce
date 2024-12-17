namespace e_commerce.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string ShortDescription {  get; set; }

        public string category {  get; set; }

        public string ImgUrl { get; set; }
    }
}
