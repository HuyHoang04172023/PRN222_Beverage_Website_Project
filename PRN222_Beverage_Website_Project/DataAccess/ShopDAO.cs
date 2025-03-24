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
        public List<Shop> GetShops()
        {
            return _context.Shops
                .Include(s => s.StatusShop)
                .ToList();
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
                .Include(s => s.Products)
                .Include(s => s.StatusShop)
                .FirstOrDefault();
        }

        public List<Shop>? GetShopsPending()
        {
            return _context.Shops
                .Where(s => s.StatusShop.StatusShopName == "pending")
                .ToList();
        }

        public void UpdateStatusShopByShopId(int shopID, int idStatusShop)
        {
            var shop = _context.Shops.FirstOrDefault(s => s.ShopId == shopID);
            if (shop != null)
            {
                shop.StatusShopId = idStatusShop;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Không tìm thấy cửa hàng.");
            }
        }

        public void UpdateApprovedByOfShop(Shop updatedShop)
        {
            var existingShop = _context.Shops.Find(updatedShop.ShopId);

            if (existingShop == null)
            {
                throw new Exception("Cửa hàng không tồn tại.");
            }

            existingShop.ApprovedBy = updatedShop.ApprovedBy;

            _context.SaveChanges();
        }

        public List<Shop>? GetShopsByStatusShopName(string statusShopName)
        {
            return _context.Shops
                .Where(s => s.StatusShop.StatusShopName == statusShopName)
                .ToList();
        }
    }
}
