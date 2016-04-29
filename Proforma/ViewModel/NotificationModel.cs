using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    enum Status
    {
        Read,
        UnRead,
        Deleted
    }
    public class NotificationModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<NotificationDetailsModel> lstNotifications { get; set; }        
    }
    public class NotificationDetailsModel
    {
        public long NotificationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public string NotificationReceived { get; set; }
    }
}