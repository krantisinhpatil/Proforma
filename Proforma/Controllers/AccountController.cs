using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Proforma.ViewModel;
using BCrypt.Net;
using Proforma.Utils;
using Proforma.Filters;
using Proforma.Results;
using System.Net.Http.Formatting;
using System.Web;
using Proforma.DAL;
using System.Web.Security;

namespace Proforma.Controllers
{
    public class AccountController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        //Web Service Method
        [HttpGet]
        //[ActionName("VerifyEmailAddress")]
        public HttpResponseMessage VerifyEmailAddress(string EmailAddress)
        {
            // var service1 = new UserVerificationService.ProformaUserServiceClient();

            var response = new EmailVerificationResponse();
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var user = _db.SourcingGuideDevUsers.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());
                if (null == user)
                {
                    response.MESSAGE = "User Not Verified";
                    response.Flag = "false";
                }
                else
                {
                    response.MESSAGE = "User Verified";
                    response.Flag = "true";
                }
                //string serviceResponse = service1.VerifyUser(EmailAddress);
                //  if (!string.IsNullOrEmpty(serviceResponse) && "User Not Verified" != serviceResponse)
                //  {
                //      response.Flag = "true";
                //  }
                //  else
                //  {
                //      response.Flag = "false";
                //  }
                //  response.MESSAGE = serviceResponse;
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        /*  [HttpGet]
          //[ActionName("VerifyEmailAddress")]
          public HttpResponseMessage VerifyEmailAddress(string EmailAddress)
          {
              // var service1 = new ServiceReference1.Service1Client();          

              var response = new EmailVerificationResponse();
              if (!string.IsNullOrEmpty(EmailAddress))
              {
                  //string serviceResponse = service1.VerifyUser(EmailAddress);
                  var _emailAddress = _db.Users.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());
                  if (null == _emailAddress)
                  {
                      response.Flag = "true";
                      response.MESSAGE = "Email Verified";
                  }
                  else
                  {
                      response.Flag = "false";
                      response.MESSAGE = "Email Not Verified";
                  }
              }
              else
              {
                  response.Flag = "false";
                  response.MESSAGE = "Insufficient data";
              }

              return Request.CreateResponse(HttpStatusCode.OK, response);
          }*/

        [HttpPost]
        // [ActionName("RegisterUser")]
        //public HttpResponseMessage RegisterUser(string EmailAddress, string Password)
        public HttpResponseMessage RegisterUser()
        {
            string EmailAddress = HttpContext.Current.Request.Form["EmailAddress"];
            string Password = HttpContext.Current.Request.Form["Password"];
            string DeviceType = HttpContext.Current.Request.Form["DeviceType"];
            string DeviceToken = HttpContext.Current.Request.Form["DeviceToken"];
            var response = new RegisterResponse();
            response.Flag = "false";
            response.UserId = 0;

            if (!string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password))
            {
                MembershipCreateStatus createStatus;
                MembershipUser newUser = Membership.CreateUser(EmailAddress, Password, EmailAddress, "question", "answer", true, out createStatus);

                if (null != newUser)
                {
                    ProformaUsers obj = new ProformaUsers();
                    obj.AspUserId = new Guid(newUser.ProviderUserKey.ToString());

                    obj.DeviceToken = DeviceToken;
                    obj.DeviceType = DeviceType;
                    obj.CreatedDate = DateTime.Now;
                    _db.ProformaUsers.Add(obj);
                    _db.SaveChanges();
                    response.Flag = "true";                    
                    response.UserId = Convert.ToInt32(obj.UserID);
                }
                //else
                //{
                //    response.MESSAGE = "Failed to register";
                //}
                response.MESSAGE = createStatus.ToString();
            }
            else
            {
                response.MESSAGE = "Failed to register";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        //[ActionName("Login")]
        //public HttpResponseMessage Login(string UserName,string Password)
        public HttpResponseMessage Login()
        {
            string UserName = HttpContext.Current.Request.Form["UserName"];
            string Password = HttpContext.Current.Request.Form["Password"];
            string DeviceType = HttpContext.Current.Request.Form["DeviceType"];
            string DeviceToken = HttpContext.Current.Request.Form["DeviceToken"];
            var response = new LoginResponse();
            response.Flag = "false";
            response.UserId = 0;

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {

                if (Membership.ValidateUser(UserName, Password))
                {
                    MembershipUser currentUser = Membership.GetUser(UserName);
                    Guid currentUserId = new Guid(currentUser.ProviderUserKey.ToString());

                    var userrecord = _db.ProformaUsers.FirstOrDefault(a => a.AspUserId == currentUserId);
                    if (null != userrecord)
                    {
                        //bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(Password, userrecord.Password);
                       // if (IsPasswordValid)
                       // {
                            userrecord.IsActive = true;
                            userrecord.DeviceType = DeviceType;
                            userrecord.DeviceToken = DeviceToken;

                            _db.Entry(userrecord).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                            response.Flag = "true";
                            response.MESSAGE = "Login successful";
                            response.UserId = Convert.ToInt32(userrecord.UserID);
                        //}
                       // else
                       // {
                        //    response.MESSAGE = "Invalid Password";
                       // }

                    }

                    else
                    {
                        response.MESSAGE = "Invalid Email Address";
                    }
                }
                else
                {
                    response.MESSAGE = "Login fail";
                }
            }
            else
            {
                response.MESSAGE = "Login fail";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        //[ActionName("ForgotPassword")]
        //public HttpResponseMessage ForgotPassword(string EmailAddress)
        public HttpResponseMessage ForgotPassword()
        {
            string EmailAddress = HttpContext.Current.Request.Form["EmailAddress"];
            var response = new ForgotPasswordResponse();
            if (!string.IsNullOrEmpty(EmailAddress))
            {

                MembershipUser currentUser = Membership.GetUser(EmailAddress, false);
                string newPassword = string.Empty;

                //var user = _db.ProformaUsers.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());
                if (null == currentUser)
                {
                    response.Flag = "false";
                    response.MESSAGE = "Email not exists";
                }
                else
                {
                    newPassword = currentUser.ResetPassword();
                    Guid currentUserId = new Guid(currentUser.ProviderUserKey.ToString());
                    // var tempPass = KeyGenerator.GetUniqueKey(10);
                    var user = _db.ProformaUsers.FirstOrDefault(a => a.AspUserId == currentUserId);
                    user.TempPassword = newPassword;
                    user.TempPassActiveTill = DateTime.Now.AddDays(1); //Active for one week.
                    _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    response.Flag = "true";
                    response.MESSAGE = "Temporary code generated";
                    response.TemporaryPassword = newPassword;

                    //Send Mail
                    string from = "nilesh.manglekar.proforma@gmail.com", from_name = "Proforma", to = EmailAddress, cc = "", bcc = "", subject = "Forgot Password";
                    string body = "<div><p>Hello " + EmailAddress + "</p></div><div><p>Your new temporary code is <b>" + newPassword + "</b></p><p>NOTE: This code is valid until <b>" + user.TempPassActiveTill + "</b>. Please use this code for first login and reset your password.</p></div>";
                    bool isHtml = true;
                    Emails.SendEmail(from, from_name, to, cc, bcc, subject, body, isHtml);
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        //[ActionName("ResetPassword")]
        // public HttpResponseMessage ResetPassword(string TemporaryPassword, string NewPassword)
        public HttpResponseMessage ResetPassword()
        {
            string TemporaryPassword = HttpContext.Current.Request.Form["TemporaryPassword"];
            string NewPassword = HttpContext.Current.Request.Form["NewPassword"];
            string EmailAddress = HttpContext.Current.Request.Form["EmailAddress"];
            bool changePasswordSucceeded = false;

            var response = new ResetPasswordResponse();
            if (!string.IsNullOrEmpty(TemporaryPassword))
            {
                MembershipUser currentUser = Membership.GetUser(EmailAddress);
               
                if (null == currentUser)
                {
                    response.Flag = "false";
                    response.MESSAGE = "Invalid data";
                }
                else
                {
                    Guid currentUserId = new Guid(currentUser.ProviderUserKey.ToString());
                    var user = _db.ProformaUsers.FirstOrDefault(a => a.AspUserId== currentUserId);

                    if (null != user && null != user.TempPassActiveTill && DateTime.Now < user.TempPassActiveTill)
                    {
                        changePasswordSucceeded = currentUser.ChangePassword(TemporaryPassword, NewPassword);
                        if (changePasswordSucceeded)
                        {
                            // var hashed = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                            // user.Password = hashed;
                            user.TempPassword = string.Empty;
                            user.TempPassActiveTill = null;
                            _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();

                            response.Flag = "true";
                            response.MESSAGE = "Password reset successful";

                            //Send Mail
                            string from = "nilesh.manglekar.proforma@gmail.com", from_name = "Proforma", to = EmailAddress, cc = "", bcc = "", subject = "Reset Password";
                            string body = "<div><p>Hello " + EmailAddress + "</p></div><div><p>Your password has been changed successfully.</p></div>";
                            bool isHtml = true;
                            Emails.SendEmail(from, from_name, to, cc, bcc, subject, body, isHtml);
                        }
                        else
                        {
                            response.Flag = "false";
                            response.MESSAGE = "Temporary password expired";
                        }
                    }
                    else
                    {
                        response.Flag = "false";
                        response.MESSAGE = "Temporary password expired";
                    }
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [HttpPost]
        public HttpResponseMessage AddToFavourites()
        {
            string UserId = HttpContext.Current.Request.Form["UserId"];
            string CompanyId = HttpContext.Current.Request.Form["CompanyId"];
            AddToFavouritesResponse _Response = new AddToFavouritesResponse();
            _Response.Flag = "false";

            if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(CompanyId))
            {
                var uid = Convert.ToInt32(UserId);
                var cid = Convert.ToInt32(CompanyId);
                var uv = _db.UserFavorites.FirstOrDefault(u => u.UserId == uid && u.CompanyId == cid);
                if (null == uv)
                {
                    UserFavorite _UserFavorite = new UserFavorite();
                    _UserFavorite.CompanyId = Convert.ToInt32(CompanyId);
                    _UserFavorite.UserId = Convert.ToInt64(UserId);
                    _db.UserFavorites.Add(_UserFavorite);
                    _db.SaveChanges();
                    _Response.MESSAGE = "Added to favorite";
                    _Response.Flag = "true";
                }
                else
                {
                    _db.UserFavorites.Remove(uv);
                    _db.SaveChanges();
                    _Response.Flag = "true";
                    _Response.MESSAGE = "Company removed from favorites";
                }
            }
            else
            {
                _Response.Flag = "Insufficient data";
            }

            return Request.CreateResponse(HttpStatusCode.OK, _Response);
        }

        [HttpGet]
        public HttpResponseMessage GetUserFavourites(long? UserId)
        {
            UserFavouritesModel _UserFavoritesModel = new UserFavouritesModel();
            _UserFavoritesModel.MESSAGE = "Insufficient data";
            _UserFavoritesModel.Flag = "false";
            if (null != UserId)
            {
                var userFavorites = _db.UserFavorites.Where(a => a.UserId == UserId);

                if (null != userFavorites)
                {
                    List<CompanyDetailsViewModel> _lstCompanyViewModel = new List<CompanyDetailsViewModel>();
                    _UserFavoritesModel.MESSAGE = "Favorite companies";
                    _UserFavoritesModel.Flag = "true";
                    foreach (var userfav in userFavorites)
                    {
                        var company = _db.Companies.FirstOrDefault(b => b.CompanyId == userfav.CompanyId);
                        if (null != company)
                        {
                            var _CompanyViewModel = Common.GetCompanyDetails(company);
                            _CompanyViewModel.IsFavourite = true;

                            _lstCompanyViewModel.Add(_CompanyViewModel);
                        }
                    }
                    _UserFavoritesModel.Companies = _lstCompanyViewModel;
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK, _UserFavoritesModel);
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword()
        {
            var UserID = Convert.ToInt32(HttpContext.Current.Request.Form["UserID"]);
            string OldPassword = HttpContext.Current.Request.Form["OldPassword"];
            string NewPassword = HttpContext.Current.Request.Form["NewPassword"];

            var response = new LoginResponse();
            response.Flag = "false";
            response.UserId = 0;
            bool changePasswordSucceeded = false;
            if (0 < UserID)
            {
                var _User = _db.ProformaUsers.FirstOrDefault(a => a.UserID == UserID);
                if (null != _User)
                {
                    MembershipUser mu = Membership.GetUser(_User.AspUserId);
                    if (mu != null)
                    {
                        MembershipUser currentUser = Membership.GetUser(mu.UserName, true /* userIsOnline */);

                        changePasswordSucceeded = currentUser.ChangePassword(OldPassword, NewPassword);

                        if(changePasswordSucceeded)
                        {
                            _User.ModifiedDate = DateTime.Now;
                            _db.Entry(_User).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                            response.Flag = "true";
                            response.MESSAGE = "Password changed successfully";
                            response.UserId = Convert.ToInt32(_User.UserID);
                        }
                        else
                        {
                            response.MESSAGE = "Invalid Password";
                        }
                    }
                    else
                    {
                        response.MESSAGE = "User not exists";
                    }

                    //bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(OldPassword, _User.Password);
                    //if (IsPasswordValid)
                    //{
                    //    var hashed = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                    //    _User.Password = hashed;
                    //    _User.ModifiedDate = DateTime.Now;
                    //    _db.Entry(_User).State = System.Data.Entity.EntityState.Modified;
                    //    _db.SaveChanges();
                    //    response.Flag = "true";
                    //    response.MESSAGE = "Password changed successfully";
                    //    response.UserId = Convert.ToInt32(_User.UserID);
                    //}
                    //else
                    //{
                    //    response.MESSAGE = "Invalid Password";
                    //}

                }

                else
                {
                    response.MESSAGE = "User not exists";
                }
            }
            else
            {
                response.MESSAGE = "Password change failed";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }

}
