using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class SuperAdminModel
    {
        public ProformaPLPModel PLPs { get; set; }
        public List<ProformaEventsModel> Events { get; set; }
        public ProformaOwnersModel Owners { get; set; }
        public ProformaAnalyticsModel Analytics { get; set; }
    }

    public class ProformaPLPModel
    {
        public int CategoryId { get; set; }
        //public List<PLPCompanyModel> PLPCompanies { get; set; }
        public List<PLPCompanySortedModel> PLPCompanies { get; set; }
    }

    public class ProformaEventsModel
    {
        public long EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        //public DateTime? EventStartDate { get; set; }
        //public DateTime? EventEndDate { get; set; }
        public string EventImage { get; set; }
        public string ShortDescription { get; set; }
    }

    public class ProformaOwnersModel
    {

    }

    public class ProformaAnalyticsModel
    {

    }
}