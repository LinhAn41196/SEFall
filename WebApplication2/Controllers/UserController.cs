using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using(OurContext db = new OurContext())
            {
                return View(db.users.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                using (OurContext db = new OurContext())
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = user.Email + " successfully registered.";
            }
            else
            {
                ModelState.AddModelError("", "Some error occurred");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using(OurContext db = new OurContext())
            {
                var usr = db.users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
                if(usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("LoggedIn", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (OurContext dc = new OurContext())
            {
                var v = dc.users.Where(a => a.Email == email).FirstOrDefault();
                return v != null;

            }
        }
    }
}