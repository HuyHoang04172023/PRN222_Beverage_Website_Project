using System.Reflection.Metadata.Ecma335;
using PRN222_Beverage_Website_Project.DataAccess;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class ConfigDataRepository : IConfigDataRepository
    {
        private readonly ConfigDataDAO _dao;
        public ConfigDataRepository()
        {
            _dao = new ConfigDataDAO();
        }
        public int? GetRoleIdByRoleName(string roleName)
        {
            return _dao.GetRoleIdByRoleName(roleName);
        }

        public int? GetStatusShopIdByStatusShopName(string statusShopName)
        {
            return _dao.GetStatusShopIdByStatusShopName(statusShopName);
        }
        public int? GetStatusProductIdByStatusProductName(string statusProductName)
        {
            return _dao.GetStatusProductIdByStatusProductName(statusProductName);
        }
    }
}
