using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using MvcUI.Models;
using Ninject;
using PhotoGallery.Models;

namespace MvcUI.Controllers
{
    [Authorize(Roles = "Администратор, Пользователь")]
    public class UserController : Controller
    {
        [Inject]
        private readonly IUserCreationService service;
        [Inject]
        private readonly IImageService imageService;
        [Inject]
        private readonly IUserQueryService queryService;
        [Inject]
        private readonly IRoleQueryService roleQueryService;

        public UserController(IUserCreationService service, IImageService imageService, IUserQueryService queryService, IRoleQueryService roleQueryService)
        {
            if (service == null)
            {
                throw new System.ArgumentNullException("subjectQueryService", "Subject auery service is null.");
            }
            this.service = service;
            this.imageService = imageService;
            this.queryService = queryService;
            this.roleQueryService = roleQueryService;
        }
        //
        // GET: /User/

        public ActionResult Index()
        {

            List<User> usersBll = queryService.GetAllUsers().ToList();

            List<ShowUserInfo> usersWeb = new List<ShowUserInfo>();

            foreach (var user in usersBll)
            {
                ShowUserInfo temp = new ShowUserInfo
                {
                    Name = user.Name,
                    Email = user.Email,
                    Pass = user.Password,
                    RoleName = user.Role.Name
                };
                usersWeb.Add(temp);
            }

            return View(usersWeb);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int id)
        {
            string sqlQuery = "SELECT * FROM [dbo].[ShowUserInfo]() WHERE Id = {0}";
            Object[] parameters = { id };

            var users = queryService.GetUser(id.ToString());

            //SelectList roles = roleQueryService.GetAllRoles();
            //ViewBag.Roles = roles;

            
                ShowUserInfo temp = new ShowUserInfo
                {
                    Id = Convert.ToInt32(users.Id),
                    Name = users.Name,
                    Email = users.Email,
                    Pass = users.Password,
                    RoleName = users.Role.Name
                };
            
            return View(temp);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(ShowUserInfo user)
        {
            
                /*User userTmp = db.Users.Find(user.Id);
                userTmp.Name = user.Name;
                userTmp.Email = user.Email;
                userTmp.Password = user.Pass;

                UserRole userRole = (from u in db.UsersRoles
                                     where u.UserId == user.Id
                                     select u).FirstOrDefault();

                userRole.RoleId = user.IdRoleName;

                db.Entry(userTmp).State = EntityState.Modified;
                db.Entry(userRole).State = EntityState.Modified;

                db.SaveChanges();*/
                return RedirectToAction("Index");

        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int id)
        {
            /*User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }

        public ActionResult Files()
        {
            var images = new ImagesModel();
            //Read out files from the files directory
            var files = Directory.GetFiles(Server.MapPath("~/Content/img"));
            //Add them to the model

            List<string> nameImages = new List<string>();
            List<string> titleImages = new List<string>();
            List<int> IdImages = new List<int>();


            User user = queryService.GetUserByEmail(User.Identity.Name);

            List<UserImage> userImages = imageService.GetUserImages(User.Identity.Name).ToList();


                foreach (var image in userImages)
                {
                    nameImages.Add(image.PhotoFilePath);
                    titleImages.Add(image.Title);
                    IdImages.Add(image.Id);
                }
            

            foreach (var file in files)
            {
                if (nameImages.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    images.Images.Add(Path.GetFileName(file));
                    images.ImagesName.Add(titleImages[nameImages.IndexOf(Path.GetFileNameWithoutExtension(file))]);
                    images.IdImages.Add(IdImages[nameImages.IndexOf(Path.GetFileNameWithoutExtension(file))]);
                    images.FullPath.Add(Path.GetPathRoot(file));
                }

            }

            return View(images);
        }

    }
}
