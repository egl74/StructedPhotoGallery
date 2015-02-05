using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Concreate.Mapping;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;

namespace BLL.Concreate.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public ImageService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public void AddUserImage(string email, string filePath, string title, string description)
        {
            userRepository.AddUserImage(email, filePath, title, description);
        }

        public IEnumerable<UserImage> GetUserImages(string email)
        {
            var image = userRepository.GetUserImages(email);

            return image.Select(userImage => userImage.ToBll()).ToList();
        }

        public void DeleteUserImage(string email, int id)
        {
            userRepository.DeleteUserImage(email, id);
        }

        public IEnumerable<UserImage> GetAllImages()
        {
            var image = userRepository.GetAllImages();

            return image.Select(userImage => userImage.ToBll()).ToList();
        }
    }
}
