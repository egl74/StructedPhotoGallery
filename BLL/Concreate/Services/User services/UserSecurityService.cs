using BLL.Interface.Abstract;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services
{
    public class UserSecurityService: IUserSecurityService
    {

        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserSecurityService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public bool ValidateUser(string email, string password)
        {
            return userRepository.ValidateUser(email, password);
        }
    }
}
