using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class ProformaEventResponseModel
    {
        public long EventId { get; set; }
        public string EventTitle { get; set; }
        public string Description { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string Message { get; set; }
        public string EventImage { get; set; }
    }
}