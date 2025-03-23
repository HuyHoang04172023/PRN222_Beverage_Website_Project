﻿using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IShopRepository
    {
        public void AddShop(Shop shop);
        public void UpdateShop(Shop updatedShop);
        public void DeleteShop(int shopId);
        public Shop? GetShopByUserID(int userId);
        public Shop? GetShopByShopID(int shopID);
        public List<Shop>? GetShopsPending();
        public void UpdateStatusShopByShopId(int shopID, int idStatusShop);
        public void UpdateApprovedByOfShop(Shop updatedShop);
        public List<Shop>? GetShopsByStatusShopName(string statusShopName);
    }
}
