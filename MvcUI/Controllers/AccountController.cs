using System.Web.Mvc;
using System.Web.Security;
using MvcUI.Infrastructure;
using MvcUI.Models;

namespace MvcUI.Controllers
{
        [AllowAnonymous]
        public class AccountController : Controller
        {
            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Login(LogViewModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный пароль или логин");
                    }
                }
                return View(model);
            }

            public ActionResult LogOff()
            {
                FormsAuthentication.SignOut();

                return RedirectToAction("Login", "Account");
            }

            public ActionResult Register()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Register(RegisterModel model)
            {
                if (ModelState.IsValid)
                {
                    MembershipUser membershipUser =
                        ((PhotoGalleryMembershipProvider) Membership.Provider).CreateUser(model.Name ,model.Email, model.Password);

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ошибка при регистрации");
                    }
                }
                return View(model);
            }

            public ActionResult Manage()
            {               
                return View();
            }

        }
}
