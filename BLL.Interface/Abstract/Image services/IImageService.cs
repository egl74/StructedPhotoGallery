using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Abstract
{
    public interface IImageService
    {
        void AddUserImage(string email, string filePath, string title, string description);
        IEnumerable<UserImage> GetUserImages(string email);
        void DeleteUserImage(string email, int id);
        IEnumerable<UserImage> GetAllImages();
    }
}
