﻿using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IConfigDataService
    {
        public int? GetRoleIdByRoleName(string roleName);
        public int? GetStatusShopIdByStatusShopName(string statusShopName);
        public int? GetStatusProductIdByStatusProductName(string statusProductName);
        public List<ProductSize> GetProductSizes();
    }
}
