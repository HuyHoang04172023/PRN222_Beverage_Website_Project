using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _dao;
        public ProductRepository()
        {
            _dao = new ProductDAO();
        }
        public List<Product> GetProductsByShopId(int shopId)
        {
            return _dao.GetProductsByShopId(shopId);
        }
    }
}
