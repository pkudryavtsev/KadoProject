using ProductDb;

namespace Repository
{
    public class RP
    {
        private readonly ProductDbContext _productContext;
        public ProductDbContext ProductContext { get { return _productContext; } }

        public RP(ProductDbContext productContext)
        {
            _productContext = productContext;
        }
    }
}