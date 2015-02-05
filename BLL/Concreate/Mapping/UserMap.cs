using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Concreate.Mapping
{
    public static class UserMap
    {
        public static DAL.Interface.Entities.User ToDal(this User item, string password)
        {
            return new DAL.Interface.Entities.User
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Password = password,
            };
        }
        public static DAL.Interface.Entities.User ToDal(this User item)
        {
            return new DAL.Interface.Entities.User
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Password = item.Password
            };
        }
        public static User ToBll(this DAL.Interface.Entities.User item)
        {
            return new User
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Role = item.Role.Value.ToBll(),
                UserImages = item.UserImages == null ? new List<UserImage>() : item.UserImages.Value.Select(r => r.ToBll()).ToList()
            };
        }

        public static UserImage ToBll(this DAL.Interface.Entities.UserImage item)
        {
            return new UserImage()
            {
                Id = item.Id,
                PhotoFilePath = item.PhotoFilePath,
                Title = item.Title,
                Description = item.Description
            };
        }


        public static Role ToBll(this DAL.Interface.Entities.Role item)
        {
            return new Role
            {
                Id = item.Id,
                Name = item.Name,
            };
        }

        public static ORM.Model.Role ToOrm(this Role item)
        {
            return new ORM.Model.Role()
            {
                RoleId = item.Id,
                RoleName = item.Name,
            };
        }

        public static UserImage ToDal(this ORM.Model.UserImage item)
        {
            return new UserImage()
            {
                Id = item.ImageId,
                PhotoFilePath = item.PhotoFilePath,
                Title = item.Title,
                Description = item.Description
            };
        }

        public static Guid ToGuid(this string item)
        {
            Guid result;
            if (Guid.TryParse(item, out result))
            {
                return result;
            }
            return new Guid();
        }

    }
}
