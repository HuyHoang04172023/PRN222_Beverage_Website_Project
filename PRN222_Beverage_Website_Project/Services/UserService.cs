using PRN222_Beverage_Website_Project.Models;
using PRN222_Beverage_Website_Project.Repositories;

namespace PRN222_Beverage_Website_Project.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService()
        {
            _repository = new UserRepository();
        }

        public List<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public User CheckUserLogin(string email, string password)
        {
            return _repository.CheckUserLogin(email, password);
        }
    }
}
