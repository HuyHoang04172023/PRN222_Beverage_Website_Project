using PRN222_Beverage_Website_Project.DataAccess;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleDAO _dao;
        public RoleRepository()
        {
            _dao = new RoleDAO();
        }

        public int? GetRoleIdByRoleName(string roleName)
        {
            return _dao.GetRoleIdByRoleName(roleName);
        }
    }
}
