using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services.Role_services
{
    public class UserRolesManagementService:IUserRolesManagementService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserRolesManagementService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public void AddUserRole(string email, string roleName)
        {
            userRepository.AddUserRole(email, roleName);
        }

        public void ChangeUserRole(string email, string roleName)
        {
            userRepository.ChangeUserRole(email, roleName);
        }
    }
}
