using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Abstract
{
    public interface IUserQueryService
    {
        User GetUserByEmail(string email);
        User GetUser(string id);
        IEnumerable<User> GetAllUsers();
    }
}
