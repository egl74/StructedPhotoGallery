using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Abstract
{
    public interface IUserCreationService
    {
        void CreateUser(string name, string email, string password);
        bool DeleteUser(string email);
        void UpdateUser(User user);

        Role GetUserRole(string email);
        void AddUserRole(string email, string roleName);
        void DeleteUserRole(string email, string roleName);
        void AddRoleName(string roleName); 
    }
}
