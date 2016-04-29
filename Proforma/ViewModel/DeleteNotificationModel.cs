using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class DeleteNotificationModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public long NotificationId { get; set; }
        public string DeleteStatus { get; set; }
    }
}