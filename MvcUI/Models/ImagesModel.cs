using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class ImagesModel
    {
        public ImagesModel()
        {
            IdImages = new List<int>();
            Images = new List<string>();
            ImagesName = new List<string>();
            Description = new List<string>();
            FullPath = new List<string>();
        }

        public List<int> IdImages { get; set; }

        public List<string> Images { get; set; }

        public List<string> ImagesName { get; set; }
 
        public List<string> Description { get; set; }

        public List<string> FullPath { get; set; } 

    }
}