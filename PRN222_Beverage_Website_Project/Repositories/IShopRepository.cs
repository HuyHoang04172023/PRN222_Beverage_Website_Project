using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IShopRepository
    {
        public void AddShop(Shop shop);
        public void UpdateShop(Shop updatedShop);
        public void DeleteShop(int shopId);
        public Shop? GetShopByUserID(int userId);
        public Shop? GetShopByShopID(int shopID);
    }
}
