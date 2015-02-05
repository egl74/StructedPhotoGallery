using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Concreate.Mapping
{
    public static class UserImageMap
    {
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

        public static ORM.Model.UserImage ToOrm(this DAL.Interface.Entities.UserImage item)
        {
            return new ORM.Model.UserImage
            {
                ImageId = item.Id,
                PhotoFilePath = item.PhotoFilePath,
                Title = item.Title,
                Description = item.Description
            };
        }
    }
}
