using PRN222_Beverage_Website_Project.DataAccess;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _dao;
        public UserRepository()
        {
            _dao = new UserDAO();
        }

        public List<User> GetUsers()
        {
            return _dao.GetUsers();
        }

        public User CheckUserLogin(string email, string password)
        {
            return _dao.CheckUserLogin(email, password);
        }
    }
}
