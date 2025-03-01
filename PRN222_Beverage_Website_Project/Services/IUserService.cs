using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User CheckUserLogin(string email, string password);
    }
}
