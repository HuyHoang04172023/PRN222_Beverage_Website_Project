﻿using PRN222_Beverage_Website_Project.DataAccess;
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

        public List<Shop> GetShops()
        {
            return _dao.GetShops();
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

        public List<Shop>? GetShopsPending()
        {
            return _dao.GetShopsPending();
        }

        public void UpdateStatusShopByShopId(int shopID, int idStatusShop)
        {
            _dao.UpdateStatusShopByShopId(shopID, idStatusShop);
        }

        public void UpdateApprovedByOfShop(Shop updatedShop)
        {
            _dao.UpdateApprovedByOfShop(updatedShop);
        }

        public List<Shop>? GetShopsByStatusShopName(string statusShopName)
        {
            return _dao.GetShopsByStatusShopName(statusShopName);
        }

        public List<Shop> SearchShopByShopName(string keyword)
        {
            return _dao.SearchShopByShopName(keyword);
        }
    }
}
