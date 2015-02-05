using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services
{
    public class RoleQueryService:IRoleQueryService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public RoleQueryService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public List<string> GetAllRoles()
        {
            var allRoles = userRepository.GetAllRoles();

            return allRoles.Select(allRole => allRole.Name).Distinct().ToList();
        }

        public bool RoleExists(string roleName)
        {
            return userRepository.RoleExists(roleName);
        }
    }
}
