using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class EventInfoModel
    {
        public string EventId { get; set; }
        public string EventTitle { get; set; }
        public string Description { get; set; }
        public string EventStartDate { get; set; }
        public string EventEndDate { get; set; }
    }
}