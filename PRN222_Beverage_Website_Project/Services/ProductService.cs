using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public List<Product> GetProductsByShopId(int shopId)
        {
            return _repository.GetProductsByShopId(shopId);
        }
    }
}
