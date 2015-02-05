using BLL.Concreate.Mapping;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services
{
    public class UserRolesQueryService:IUserRolesQueryService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserRolesQueryService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public Role GetUserRole(string email)
        {
            return userRepository.GetUserRole(email).ToBll();
        }

        public void AddRoleName(string roleName)
        {
            userRepository.AddRoleName(roleName);
        }

        public bool IsUserInRole(string email, string roleName)
        {
            bool result = userRepository.GetUserRole(email).Name == roleName;

            return result;
        }
    }
}
