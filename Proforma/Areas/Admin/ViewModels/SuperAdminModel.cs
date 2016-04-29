using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class SuperAdminModel
    {
        public SuperAdminModel()
        {
            Events = new List<ProformaEventsModel>();
            ProExclusives = new List<ProExclusiveModel>();
        }
        public ProformaPLPModel PLPs { get; set; }
        public List<ProformaEventsModel> Events { get; set; }
        public ProformaOwnersModel Owners { get; set; }
        public ProformaAnalyticsModel Analytics { get; set; }
        public List<ProExclusiveModel> ProExclusives { get; set; }
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

    public class ProExclusiveModel
    {
        public int ProExclusiveId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ThumbnailPath { get; set; }
        public string PDFTitle { get; set; }
        public string PDFFilePath { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Message { get; set; }
    }
}