using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class ConfigDataService : IConfigDataService
    {
        private readonly IConfigDataRepository _repository;
        public ConfigDataService()
        {
            _repository = new ConfigDataRepository();
        }
        public int? GetRoleIdByRoleName(string roleName)
        {
            return _repository.GetRoleIdByRoleName(roleName);
        }

        public int? GetStatusShopIdByStatusShopName(string statusShopName)
        {
            return _repository.GetStatusShopIdByStatusShopName(statusShopName);
        }
        public int? GetStatusProductIdByStatusProductName(string statusProductName)
        {
            return _repository.GetStatusProductIdByStatusProductName(statusProductName);
        }

        public List<ProductSize> GetProductSizes()
        {
            return _repository.GetProductSizes();
        }
    }
}
