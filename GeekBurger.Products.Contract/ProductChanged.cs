namespace GeekBurger.Products.Contract
{
    public class ProductChanged
    {
        public ProductState State { get; set; }
        public ProductToGet Product { get; set; }
    }
}
