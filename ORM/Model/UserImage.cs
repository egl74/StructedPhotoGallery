using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ORM.Model
{
    public class UserImage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)] 
        public int ImageId { get; set; }

        [Required]
        public string PhotoFilePath { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}