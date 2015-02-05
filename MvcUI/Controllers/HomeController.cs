using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using MvcUI.Infrastructure;
using MvcUI.Models;
using Ninject;

namespace MvcUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Inject] private readonly IUserCreationService service;
        [Inject] private readonly IImageService imageService;
        [Inject] private readonly IUserQueryService queryService;

        public HomeController(IUserCreationService service, IImageService imageService, IUserQueryService queryService)
        {
            if (service == null)
            {
                throw new System.ArgumentNullException("subjectQueryService", "Subject auery service is null.");
            }
            this.service = service;
            this.imageService = imageService;
            this.queryService = queryService;

            service.CreateUser("admin", "admin@email.com", "admin");
            service.AddUserRole("admin@email.com", "Администратор");
        }

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            var images = new ImagesModel();
            //Read out files from the files directory
            var files = Directory.GetFiles(Server.MapPath("~/Content/img"));
            //Add them to the model

            List<string> nameImages = new List<string>();
            List<string> titleImages = new List<string>();

            List<UserImage> userImages = imageService.GetAllImages().ToList();

            foreach (var image in userImages)
            {
                nameImages.Add(image.PhotoFilePath);
                titleImages.Add(image.Title);
            }


            foreach (var file in files)
            {
                if (nameImages.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    images.Images.Add(Path.GetFileName(file));
                    images.ImagesName.Add(titleImages[nameImages.IndexOf(Path.GetFileNameWithoutExtension(file))]);
                }

            }

            return View(images);
            //return RedirectToAction("Register", "Account");
        }

        [HttpPost]
        public ActionResult PreviewImage()
        {
            var bytes = new byte[0];
            ViewBag.Mime = "image/png";

            if (Request.Files.Count == 1)
            {
                bytes = new byte[Request.Files[0].ContentLength];
                Request.Files[0].InputStream.Read(bytes, 0, bytes.Length);
                ViewBag.Mime = Request.Files[0].ContentType;
            }

            ViewBag.Message = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
            return PartialView();
        }

        //
        // GET: /Home/UploadImageModal

        public ActionResult UploadImageModal()
        {
            return View();
        }

        //
        // GET: /Home/UploadImage

        public ActionResult UploadImage()
        {
            //Just to distinguish between ajax request (for: modal dialog) and normal request
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View();
        }

        //
        // POST: /Home/UploadImage

        [HttpPost]
        public ActionResult UploadImage(UploadImageModel model)
        {
            //Check if all simple data annotations are valid
            if (ModelState.IsValid)
            {
                //Prepare the needed variables
                Bitmap original = null;
                var name = "newimagefile";
                var errorField = string.Empty;

                if (model.File != null) // model.IsFile
                {
                    errorField = "File";
                    name = Path.GetFileNameWithoutExtension(model.File.FileName);
                    original = Bitmap.FromStream(model.File.InputStream) as Bitmap;
                }
                Random genRandom = new Random();

                string newName = String.Format("{0}_{1}", name, genRandom.Next(10000000));

                //If we had success so far
                if (original != null)
                {
                    var img = CreateImage(original, model.X, model.Y, model.Width, model.Height);

                    //Demo purposes only - save image in the file system
                    var fn = Server.MapPath("~/Content/img/" + newName + ".png");
                    img.Save(fn, System.Drawing.Imaging.ImageFormat.Png);

                    imageService.AddUserImage(model.UserName, newName, model.Title, model.Description);


                    //Redirect to index
                    return RedirectToAction("Index");
                }
                else //Otherwise we add an error and return to the (previous) view with the model data
                    ModelState.AddModelError(errorField,
                        "Your upload did not seem valid. Please try again using only correct images!");


            }

            return View(model);
        }

        [Authorize(Roles = "Пользователь")]
        public ActionResult Delete(int id)
        {
            User userImage = queryService.GetUserByEmail(User.Identity.Name);

            var resImage = userImage.UserImages.FirstOrDefault(p => p.Id == id);
            var files = Directory.GetFiles(Server.MapPath("~/Content/img"));

            string path = String.Format("{0}\\{1}.png", Path.GetDirectoryName(files[0]), resImage.PhotoFilePath);

            System.IO.File.Delete(path);
            imageService.DeleteUserImage(User.Identity.Name, resImage.Id);
            
            return RedirectToAction("Index", "Home");
        }



        /// <summary>
        /// Creates a small image out of a larger image.
        /// </summary>
        /// <param name="original">The original image which should be cropped (will remain untouched).</param>
        /// <param name="x">The value where to start on the x axis.</param>
        /// <param name="y">The value where to start on the y axis.</param>
        /// <param name="width">The width of the final image.</param>
        /// <param name="height">The height of the final image.</param>
        /// <returns>The cropped image.</returns>
        private Bitmap CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }

    }
}

