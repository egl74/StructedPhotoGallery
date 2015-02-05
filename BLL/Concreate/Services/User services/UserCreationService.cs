using System;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using BLL.Concreate.Mapping;

namespace BLL.Concreate.Services
{
    public class UserCreationService : IUserCreationService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserCreationService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public void CreateUser(string name, string email, string password)
        {
            var item = new DAL.Interface.Entities.User {Name = name, Email = email, Password = password};
            userRepository.Add(item);
            userRepository.AddUserRole(email, "Пользователь");
        }

        public bool DeleteUser(string email)
        {
            bool result = false;

            var user = this.userRepository.GetUserByEmail(email);

            if (user != null)
            {
                try
                {
                    this.userRepository.Delete(user);
                    result = true;
                }
                catch
                {
                    result = false;
                }   
            }
            return result;
        }

        public void UpdateUser(User user)
        {
            var dalUser = this.userRepository.GetUser(user.Id);
            if (dalUser != null)
            {
                dalUser.Name = user.Name;
                dalUser.Email = user.Email;
                this.userRepository.Update(dalUser);
            }
        }

        public User GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Role GetUserRole(string email)
        {
            return userRepository.GetUserRole(email).ToBll();
        }

        public void AddUserRole(string email, string roleName)
        {
            userRepository.AddUserRole(email,roleName);
        }

        public void DeleteUserRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public void AddRoleName(string roleName)
        {
            userRepository.AddRoleName(roleName);
        }

        public bool ValidateUser(string email, string password)
        {
            return userRepository.ValidateUser(email, password);
        }
    }
}