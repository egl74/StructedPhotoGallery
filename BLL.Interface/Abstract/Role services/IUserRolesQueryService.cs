using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Abstract
{
    public interface IUserRolesQueryService
    {
        Role GetUserRole(string email);
        void AddRoleName(string roleName); 
        bool IsUserInRole(string email, string roleName);
    }
}
