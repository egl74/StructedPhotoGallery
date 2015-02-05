using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Interface.Abstract
{
    public interface IImageRepository:IRepository<UserImage>
    {
        IEnumerable<UserImage> GetUserImages(string email);
    }
}
