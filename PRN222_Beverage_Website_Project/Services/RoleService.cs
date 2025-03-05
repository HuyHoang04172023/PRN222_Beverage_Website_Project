using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService()
        {
            _repository = new RoleRepository();
        }

        public int? GetRoleIdByRoleName(string roleName)
        {
            return _repository.GetRoleIdByRoleName(roleName);
        }
    }
}
