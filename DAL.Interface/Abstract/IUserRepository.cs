using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Abstract
{
    public interface IUserRepository:IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUser(string id);

        Role GetUserRole(string email);
        void AddUserRole(string email, string roleName);
        void ChangeUserRole(string email, string roleName);
        
        void AddRoleName(string roleName);
        bool RoleExists(string roleName);
        IEnumerable<Role> GetAllRoles();

        bool ValidateUser(string email, string password);

        void AddUserImage(string email, string filePath, string title, string description);
        IEnumerable<UserImage> GetUserImages(string email);
        void DeleteUserImage(string email, int id);
        IEnumerable<UserImage> GetAllImages();
    }
}
