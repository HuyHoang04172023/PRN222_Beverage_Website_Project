using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class UserDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public UserDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public List<User> GetUsers() => _context.Users.ToList();

        public User CheckUserLogin(string email, string password)
        {
            return _context.Users
                     .Include(u => u.Role)
                     .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool CheckEMailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
