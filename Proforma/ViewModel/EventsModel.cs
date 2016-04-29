using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class EventsModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<EventDetailsModel> EventDetailsModel { get; set; }
    }

    public class EventDetailsModel
    {
        public long EventId { get; set; }
        public string EventTitle { get; set; }
        public string Image { get; set; }
        public string EventSubHeading { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
       
    }

    public class EventDetailsViewModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<EventDetailsModel> EventDetailsModel { get; set; }
    }
}