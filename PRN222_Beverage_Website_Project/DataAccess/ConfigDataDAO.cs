using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ConfigDataDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ConfigDataDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public int? GetRoleIdByRoleName(string roleName)
        {
            return _context.Roles
                   .Where(r => r.RoleName == roleName)
                   .Select(r => r.RoleId)
                   .FirstOrDefault();
        }

        public int? GetStatusShopIdByStatusShopName(string statusShopName)
        {
            return _context.StatusShops
                   .Where(r => r.StatusShopName == statusShopName)
                   .Select(r => r.StatusShopId)
                   .FirstOrDefault();
        }

        public int? GetStatusProductIdByStatusProductName(string statusProductName)
        {
            return _context.StatusProducts
                   .Where(r => r.StatusProductName == statusProductName)
                   .Select(r => r.StatusProductId)
                   .FirstOrDefault();
        }
    }
}
