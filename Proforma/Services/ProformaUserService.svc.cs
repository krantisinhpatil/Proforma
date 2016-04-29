using Proforma.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Proforma.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProformaUserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProformaUserService.svc or ProformaUserService.svc.cs at the Solution Explorer and start debugging.
    public class ProformaUserService : IProformaUserService
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        public void DoWork()
        {
        }


        public string VerifyUser(string EmailAddress)
        {
            string _Response = "";
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var user = _db.SourcingGuideDevUsers.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());


                if (null == user)
                {
                    _Response = "User Not Verified";
                }
                else
                {
                    _Response = "User Verified";
                }
            }
            else
            {
                _Response = "Insufficient data";
            }

            return _Response;
        }
    }
}
