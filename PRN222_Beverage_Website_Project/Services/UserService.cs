﻿using PRN222_Beverage_Website_Project.Models;
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

        public bool CheckEMailExist(string email)
        {
            return _repository.CheckEMailExist(email);
        }

        public void AddUser(User user)
        {
            _repository.AddUser(user);
        }

        public void UpdateRoleIdOfUser(int userId, int roleId)
        {
            _repository.UpdateRoleIdOfUser(userId, roleId);
        }
    }
}
