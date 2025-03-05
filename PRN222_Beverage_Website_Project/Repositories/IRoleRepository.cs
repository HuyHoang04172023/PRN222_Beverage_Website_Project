namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IRoleRepository
    {
        public int? GetRoleIdByRoleName(string roleName);
    }
}
