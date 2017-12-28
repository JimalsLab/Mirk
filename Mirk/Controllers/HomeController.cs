using Mirk.Buisness;
using Mirk.DAL.Beans;
using Mirk.DAL.Services;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mirk.Controllers
{
    public class HomeController : Controller
    {

        public IUserService userService;

        public HomeController(IUserService us)
        {
            this.userService = us;
        }

        public ActionResult Index()
        {
            try
            {
                string cookieId = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieId];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                return RedirectToAction("Index", "Main");

            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult Mirk()
        {
            return View();
        }

        public ActionResult Sendmail(FeedbackViewModel fvm)
        {
            SendMail s = new SendMail();
            s.Send(fvm.Name, fvm.Email, fvm.Content);
            return View("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Mentions()
        {
            return View("Mentions");
        }

        public ActionResult VerifyLogin(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                List<UserBean> uList = userService.GetMany(us => us.Username == lvm.Username).ToList();
                if (uList.Any())
                {
                    UserBean u = uList.First();
                    if (u.Password == lvm.Password)
                    {
                        FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);

                        MainViewModel mvm = new ViewModels.MainViewModel();
                        mvm.currUser = u;

                        return RedirectToAction("Index", "Main");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password doesn't match");
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", "Username not found");
                }
                return View("Login", lvm);
                // no matching username or password
            }
            return View("Login", lvm);
        }

        public ActionResult Disconnect()
        {
            try
            {
                FormsAuthentication.SignOut();
            }
            catch (Exception e)
            {

            }
            return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View("Index");
        }

        public ActionResult RegisterVerify(RegisterViewModel e)
        {
            if (ModelState.IsValid)
            {
                if (e.Password != e.VerifyPassword)
                {
                    ModelState.AddModelError("VerifyPassword", "Passwords don't match");
                    return View("Register", e);
                }
                List<UserBean> TriListName = userService.GetAll().ToList();

                foreach(UserBean test in TriListName)
                {
                    if(test.Username.ToLower() == e.Username.ToLower())
                    {
                        ModelState.AddModelError("Username", "Username is already taken !");
                        return View("Register", e);
                    }
                }

                foreach(UserBean testmail in TriListName)
                {
                    if (testmail.Email == e.Email)
                    {
                        ModelState.AddModelError("Email", "Email is already in use !");
                        return View("Register", e);
                    }
                }

                UserBean nouveluser = new UserBean();
                nouveluser.Username = e.Username;
                nouveluser.Password = e.Password;
                nouveluser.Lastname = e.Lastname;
                nouveluser.Firstname = e.Firstname;
                nouveluser.Email = e.Email;
                nouveluser.Adress = e.Adress;
                nouveluser.Phone = e.Phone;
                nouveluser.Photo = "/User_Pictures/Default.png";

                // Commentaire inutile pour pouvoir commit oui oui oui
                List<UserBean> TriList = userService.GetAll().OrderByDescending(u => u.Id).ToList();

                if (TriList != null)
                {
                    nouveluser.Id = TriList.First().Id + 1;
                }
                else
                {
                    nouveluser.Id = 0;
                }
                nouveluser.DateCreation = DateTime.Now;
                userService.Create(nouveluser);
                return View("Index");
            }

                return View("Register", e);
        }


        #region error handling
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }
        #endregion
    }
}