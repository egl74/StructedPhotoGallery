using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Concreate.Mapping
{
    public static class MembershipMap
    {
        public static ORM.Model.User ToOrm(this User item)
        {
            return new ORM.Model.User()
            {
                UserId = item.Id.ToGuid(),
                Name = item.Name,
                Email = item.Email,
                Password = item.Password,
            };
        }

        public static User ToDal(this ORM.Model.User item)
        {
            return new User()
            {
                Id = item.UserId.ToString(),
                Name = item.Name,
                Email = item.Email,
                Password = item.Password,
                Role = new Lazy<Role>(() => item.Role == null ? null : item.Role.ToDal()),
                UserImages = new Lazy<IEnumerable<UserImage>>(() => item.UserImages == null ? new List<UserImage>() : item.UserImages.Select(r => r.ToDal()).ToList())
            };
        }


        public static Role ToDal(this ORM.Model.Role item)
        {
            return new Role()
            {
                Id = item.RoleId,
                Name = item.RoleName,
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
