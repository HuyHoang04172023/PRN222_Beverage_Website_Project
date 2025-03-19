using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ProductDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ProductDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public List<Product> GetProductsByShopId(int shopId)
        {
            return _context.Products
                .Where(p => p.ShopId == shopId)
                .OrderByDescending(p => p.ProductId)
                .Include(p => p.StatusProduct)
                .ToList();
        }
    }
}
