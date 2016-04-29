using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class NotificationsController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        [HttpGet]
        //[ActionName("GetUserNotifications")]
        public HttpResponseMessage GetUserNotifications(int UserId)
        {
            var userNotifications = _db.UserNotifications.Where(a => a.UserId == UserId);
            NotificationModel _Notification = new NotificationModel();
            _Notification.MESSAGE = "User Notifications";
            _Notification.Flag = "false";
            List<NotificationDetailsModel> _lstNotificationDetails = new List<NotificationDetailsModel>();
            if (null!= userNotifications)
            {
               
                _Notification.Flag = "true";
                foreach (var noti in userNotifications)
                {
                    NotificationDetailsModel _NotificationModel = new NotificationDetailsModel();
                    var _NotificationDetail = _db.Notifications.FirstOrDefault(b => b.NotificationId == noti.NotificationId && noti.Status!=Status.Deleted.ToString());
                    if (null != _NotificationDetail)
                    {
                        _NotificationModel.NotificationId = _NotificationDetail.NotificationId;
                        _NotificationModel.Title =""+ _NotificationDetail.Title;
                        _NotificationModel.Description = "" + _NotificationDetail.Description;
                        _NotificationModel.NotificationType = "" + _NotificationDetail.NotificationType;
                        _NotificationModel.Status = "" + noti.Status;
                        _NotificationModel.NotificationReceived = TimeAgo(noti.ReceivedDate);
                        _lstNotificationDetails.Add(_NotificationModel);
                    }
                }
               
            }
            _Notification.lstNotifications = _lstNotificationDetails;
            return Request.CreateResponse(HttpStatusCode.OK, _Notification);
        }

        [HttpGet]
       // [ActionName("DeleteUserNotification")]
        public HttpResponseMessage DeleteUserNotification(long UserId, long NotificationId)
        {
            DeleteNotificationModel _DeleteNotification = new DeleteNotificationModel();
            _DeleteNotification.MESSAGE = "Delete User Notification";
            _DeleteNotification.Flag = "false";
            var _NotificationToDelete = _db.UserNotifications.FirstOrDefault(a => a.UserId == UserId && a.NotificationId == NotificationId);
            if(null!= _NotificationToDelete)
            {
                _DeleteNotification.Flag = "true";
                _DeleteNotification.NotificationId = NotificationId;
                _NotificationToDelete.Status = Status.Deleted.ToString();
                _NotificationToDelete.ModifiedBy = UserId;
                _NotificationToDelete.ModifiedDate = DateTime.Now;
                _db.Entry(_NotificationToDelete).State = EntityState.Modified;
                _db.SaveChanges();
                _DeleteNotification.DeleteStatus = "Notification deleted successfully";
            }
            else
            {
                _DeleteNotification.DeleteStatus = "Notification was not found";
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, _DeleteNotification);
        }

        public static string TimeAgo(DateTime? dt)
        {            
            TimeSpan span = DateTime.Now - dt.Value;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("{0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}
