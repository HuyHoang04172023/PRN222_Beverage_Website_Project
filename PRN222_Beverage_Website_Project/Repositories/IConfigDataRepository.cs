namespace PRN222_Beverage_Website_Project.Repositories
{
    public interface IConfigDataRepository
    {
        public int? GetRoleIdByRoleName(string roleName);
        public int? GetStatusShopIdByStatusShopName(string statusShopName);
    }
}
