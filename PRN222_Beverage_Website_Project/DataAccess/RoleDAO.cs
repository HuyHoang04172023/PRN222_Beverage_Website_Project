using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class RoleDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public RoleDAO()
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

    }
}
