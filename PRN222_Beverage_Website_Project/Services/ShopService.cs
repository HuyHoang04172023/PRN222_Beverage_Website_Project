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
        public void AddShop(Shop shop)
        {
            _repository.AddShop(shop);
        }
    }
}
