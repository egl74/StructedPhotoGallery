using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MvcUI.Models
{
    public class UploadImageModel
    {   
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Краткое описание")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Description { get; set; }

        [Display(Name = "Выбранный файл")]
        public HttpPostedFileBase File { get; set; }

        public bool IsFile { get; set; }
        
        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }
    }
}