using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoGallery.Models
{
    public class ShowUserInfo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email-адрес")]
        [EmailAddress(ErrorMessage = "Неверно введен адрес E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Pass { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string RoleName { get; set; }

    }
}