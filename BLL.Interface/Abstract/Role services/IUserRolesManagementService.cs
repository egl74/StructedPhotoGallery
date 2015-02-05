using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IUserRolesManagementService
    {
        void AddUserRole(string email, string roleName);
        void ChangeUserRole(string email, string roleName);
    }
}
