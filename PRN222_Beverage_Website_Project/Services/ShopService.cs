using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _repository;
        public ShopService()
        {
            _repository = new ShopRepository();
        }
        public List<Shop> GetShops()
        {
            return _repository.GetShops();
        }

        public void AddShop(Shop shop)
        {
            _repository.AddShop(shop);
        }
        public void UpdateShop(Shop updatedShop)
        {
            _repository.UpdateShop(updatedShop);
        }
        public void DeleteShop(int shopId)
        {
            _repository.DeleteShop(shopId);
        }

        public Shop? GetShopByUserID(int userId)
        {
            return _repository.GetShopByUserID(userId);
        }

        public Shop? GetShopByShopID(int shopID)
        {
            return _repository.GetShopByShopID(shopID);
        }

        public List<Shop>? GetShopsPending()
        {
            return _repository.GetShopsPending();
        }

        public void UpdateStatusShopByShopId(int shopID, int idStatusShop)
        {
            _repository.UpdateStatusShopByShopId(shopID, idStatusShop);
        }

        public void UpdateApprovedByOfShop(Shop updatedShop)
        {
            _repository.UpdateApprovedByOfShop(updatedShop);
        }

        public List<Shop>? GetShopsByStatusShopName(string statusShopName)
        {
            return _repository.GetShopsByStatusShopName(statusShopName);
        }

        public List<Shop> SearchShopByShopName(string keyword)
        {
            return _repository.SearchShopByShopName(keyword);
        }
    }
}
