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
    }
}
