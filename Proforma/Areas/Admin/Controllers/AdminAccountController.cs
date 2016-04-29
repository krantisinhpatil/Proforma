using Proforma.DAL;
using Proforma.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;

namespace Proforma.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SetCompanyCategories()
        {
            var companies = _db.Companies.ToList();
            foreach (var comp in companies)
            {
                if (comp.CategoryId1 != null && comp.CategoryId1 > 0)
                {
                    CompanyCategory cCategory = new CompanyCategory();
                    cCategory.CompanyId = comp.CompanyId;
                    cCategory.CategoryId = Convert.ToInt32(comp.CategoryId1);

                    _db.CompanyCategories.Add(cCategory);
                }

            }
            _db.SaveChanges();
            return View();
        }

        public ActionResult RegisterExistingUsers()
        {
            // Attempt to register the user     

            var lstUsers = _db.ProformaUsers.Where(u => u.AspUserId == null).ToList();
            foreach (var user in lstUsers)
            {
                MembershipCreateStatus createStatus;
                MembershipUser newUser = Membership.CreateUser(user.Email, "Proforma2016!", "", "question", "answer", true, out createStatus);
                if (null != newUser)
                {
                    user.AspUserId = new Guid(newUser.ProviderUserKey.ToString());
                    _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user               
                MembershipCreateStatus createStatus;
                MembershipUser newUser = Membership.CreateUser(model.UserName, model.Password, "", "question", "answer", true, out createStatus);
                if (null != newUser)
                {
                    ProformaUsers obj = new ProformaUsers();
                    obj.AspUserId = new Guid(newUser.ProviderUserKey.ToString());
                    obj.DeviceToken = "";
                    obj.DeviceType = "";
                    obj.Email = model.UserName;
                    obj.CreatedDate = DateTime.Now;
                    _db.ProformaUsers.Add(obj);
                    _db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Companies");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Companies");

            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult CreateRole()
        {
            Roles.CreateRole("SuperAdmin");

            return View();
        }
    }
}