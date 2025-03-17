using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ShopDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ShopDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public void AddShop(Shop shop)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
        }

        public Shop? GetShopByUserID(int userId)
        {
            return _context.Shops
                .Where(s => s.CreatedBy == userId)
                .Include(s => s.StatusShop)
                .FirstOrDefault();
        }
    }
}
