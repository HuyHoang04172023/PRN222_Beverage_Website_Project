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

        public Shop? GetShopByUserID(int userId)
        {
            return _dao.GetShopByUserID(userId);
        }
    }
}
