using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Entities
{
    public class UserImage
    {
        public int Id { get; set; }

        public string PhotoFilePath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Lazy<IEnumerable<User>> Users { get; set; }
    }
}
