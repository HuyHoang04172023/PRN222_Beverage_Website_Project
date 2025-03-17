using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopDAO _dao;
        public ShopRepository()
        {
            _dao = new ShopDAO();
        }
        public void AddShop(Shop shop)
        {
            _dao.AddShop(shop);
        }
        public void UpdateShop(Shop updatedShop)
        {
            _dao.UpdateShop(updatedShop);
        }
        public void DeleteShop(int shopId)
        {
            _dao.DeleteShop(shopId);
        }
        public Shop? GetShopByUserID(int userId)
        {
            return _dao.GetShopByUserID(userId);
        }
        public Shop? GetShopByShopID(int shopID)
        {
            return _dao.GetShopByShopID(shopID);
        }
    }
}
