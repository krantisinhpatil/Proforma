using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class SPDevTeamMembersModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public string PrimaryContactNumber { get; set; }
        public List<SPDevTeamMemberDetailsModel> SPDevTeamMembersDetails { get; set; }
    }
    public class SPDevTeamMemberDetailsModel
    {
        public long SPDevTeamMemberID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Extention { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }   
}