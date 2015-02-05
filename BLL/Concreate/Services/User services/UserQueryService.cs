using System.Collections.Generic;
using BLL.Concreate.Mapping;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services
{
    public class UserQueryService: IUserQueryService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserQueryService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email).ToBll();
        }

        public User GetUser(string id)
        {
            return userRepository.GetUser(id).ToBll();
        }

        public IEnumerable<User> GetAllUsers()
        {

            IEnumerable<DAL.Interface.Entities.User> users = userRepository.Data;

            List<User> resUsers = new List<User>();

            foreach (var item in users)
            {
                resUsers.Add(item.ToBll());
            }
            return resUsers;
        }

    }
}
