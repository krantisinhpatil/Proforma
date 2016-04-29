using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class SupportCenterContactModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public SupportCenterContactDetailsModel SupportCenterContactDetails { get; set; }

    }
    public class SupportCenterContactDetailsModel
    {
        public long SupportCenterContactId { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FacebookURL { get; set; }
        public string TwitterURL { get; set; }
        public string LinkedInURL { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

}