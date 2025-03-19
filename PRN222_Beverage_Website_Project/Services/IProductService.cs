using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IProductService
    {
        public List<Product> GetProductsByShopId(int shopId);
    }
}
