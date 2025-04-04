﻿using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User CheckUserLogin(string email, string password);
        public bool CheckEMailExist(string email);
        public void AddUser(User user);
        public void UpdateRoleIdOfUser(int userId, int roleId);
    }
}
