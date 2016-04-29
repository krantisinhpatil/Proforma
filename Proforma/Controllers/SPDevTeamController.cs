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
    public class SPDevTeamController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        //[ActionName("GetSPDevTeamMembers")]
        public HttpResponseMessage GetSPDevTeamMembers()
        {
            var _SPDevTeamMembers = _db.SPDevTeamMembers.ToList();
            SPDevTeamMembersModel _SPDevTeamMembersModel = new SPDevTeamMembersModel();
            _SPDevTeamMembersModel.MESSAGE = "SP Dev Team Members";
            _SPDevTeamMembersModel.Flag = "false";
            if (null != _SPDevTeamMembers)
            {
                List<SPDevTeamMemberDetailsModel> _lstSPDevTeamMemberDetails = new List<SPDevTeamMemberDetailsModel>();
                _SPDevTeamMembersModel.Flag = "true";
                foreach (var member in _SPDevTeamMembers)
                {
                    SPDevTeamMemberDetailsModel _SPDevTeamMemberDetailsModel = new SPDevTeamMemberDetailsModel();

                    _SPDevTeamMemberDetailsModel.SPDevTeamMemberID = member.SPDevTeamMemberID;
                    _SPDevTeamMemberDetailsModel.Name = member.Name;
                    _SPDevTeamMemberDetailsModel.Title = member.Title;
                    _SPDevTeamMemberDetailsModel.Image = member.Image;
                    _SPDevTeamMemberDetailsModel.Email = member.Email;
                    _SPDevTeamMemberDetailsModel.Extention = member.Extension;
                    _SPDevTeamMemberDetailsModel.Description = member.Description;
                    _lstSPDevTeamMemberDetails.Add(_SPDevTeamMemberDetailsModel);

                }
                _SPDevTeamMembersModel.SPDevTeamMembersDetails = _lstSPDevTeamMemberDetails;
            }

            _SPDevTeamMembersModel.PrimaryContactNumber = "";
            var _SPDevTeam = _db.SPDevTeams.FirstOrDefault();
            if (null != _SPDevTeam)
            {
                _SPDevTeamMembersModel.PrimaryContactNumber = "" + _SPDevTeam.PrimaryContactNumber;
            }

            return Request.CreateResponse(HttpStatusCode.OK, _SPDevTeamMembersModel);
        }



    }
}
