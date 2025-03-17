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
        public void UpdateShop(Shop updatedShop)
        {
            var existingShop = _context.Shops.Find(updatedShop.ShopId);

            if (existingShop == null)
            {
                throw new Exception("Cửa hàng không tồn tại.");
            }

            existingShop.ShopName = updatedShop.ShopName;
            existingShop.ShopAddress = updatedShop.ShopAddress;
            existingShop.ShopImage = updatedShop.ShopImage;
            existingShop.ShopDescription = updatedShop.ShopDescription;

            _context.SaveChanges();
        }

        public void DeleteShop(int shopId)
        {
            var shop = _context.Shops.Find(shopId);

            if (shop == null)
            {
                throw new Exception("Cửa hàng không tồn tại.");
            }

            _context.Shops.Remove(shop);
            _context.SaveChanges();
        }



        public Shop? GetShopByUserID(int userId)
        {
            return _context.Shops
                .Where(s => s.CreatedBy == userId)
                .Include(s => s.StatusShop)
                .FirstOrDefault();
        }

        public Shop? GetShopByShopID(int shopID)
        {
            return _context.Shops
                .Where(s => s.ShopId == shopID)
                .Include(s => s.StatusShop)
                .FirstOrDefault();
        }
    }
}
