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
    public class GeneralSettingsController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        public HttpResponseMessage GetTermsandConditions()
        {
            TermsandConditionsModel _TermsandConditions = new TermsandConditionsModel();
            _TermsandConditions.Flag = "false";
            _TermsandConditions.MESSAGE = "Terms and Conditions";
            _TermsandConditions.TermsConditions = "";
            var _TermsCondition = _db.ProformaGeneralSettings.FirstOrDefault(a => a.Key.ToLower() == "termsconditions");
            if(null!= _TermsCondition)
            {
                _TermsandConditions.Flag = "true";
                _TermsandConditions.TermsConditions = _TermsCondition.Value;
            }
            return Request.CreateResponse(HttpStatusCode.OK, _TermsandConditions);
        }
    }
}
