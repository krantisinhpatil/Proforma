using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class SupportCenterContactController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
       // [ActionName("GetSupportCenterContact")]
        public HttpResponseMessage GetSupportCenterContact()
        {
            var _SupportCenterContacts = _db.SupportCenterContacts.FirstOrDefault();
            SupportCenterContactModel _SupportCenterContactsModel = new SupportCenterContactModel();
            _SupportCenterContactsModel.MESSAGE = "Support Center Contact";
            _SupportCenterContactsModel.Flag = "false";
            if (null != _SupportCenterContacts)
            {
                _SupportCenterContactsModel.Flag = "true";
                SupportCenterContactDetailsModel _SupportCenterContactDetailsModel = new SupportCenterContactDetailsModel();
                _SupportCenterContactDetailsModel.SupportCenterContactId = _SupportCenterContacts.SupportCenterContactId;
                _SupportCenterContactDetailsModel.PrimaryContactNumber = _SupportCenterContacts.PrimaryContactNumber;
                _SupportCenterContactDetailsModel.Email = _SupportCenterContacts.Email;
                _SupportCenterContactDetailsModel.Address = _SupportCenterContacts.Address;
                _SupportCenterContactDetailsModel.FacebookURL = _SupportCenterContacts.FacebookURL;
                _SupportCenterContactDetailsModel.TwitterURL = _SupportCenterContacts.TwitterURL;
                _SupportCenterContactDetailsModel.LinkedInURL = _SupportCenterContacts.LinkedInURL;
                _SupportCenterContactDetailsModel.CreatedDate = _SupportCenterContacts.CreatedDate;
                _SupportCenterContactsModel.SupportCenterContactDetails = _SupportCenterContactDetailsModel;
            }
            return Request.CreateResponse(HttpStatusCode.OK, _SupportCenterContactsModel);
        }
    }
}
