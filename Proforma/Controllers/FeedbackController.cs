using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class FeedbackController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpPost]
        public HttpResponseMessage SaveFeedback()
        {
            string UserId = HttpContext.Current.Request.Form["UserId"];
            string FeedbackText = HttpContext.Current.Request.Form["FeedbackText"];
           // string CreatedDate = HttpContext.Current.Request.Form["CreatedDate"];
            string Rating = HttpContext.Current.Request.Form["Rating"];
           // string Status = HttpContext.Current.Request.Form["Status"];

            FeedbackResponse _FeedbackResponse = new FeedbackResponse();
            _FeedbackResponse.Flag = "false";
            _FeedbackResponse.MESSAGE = "Insuficient Data";

            if(!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(FeedbackText))
            {
                Feedback _Feedback = new Feedback();
                _Feedback.FeedBackText = FeedbackText;
                _Feedback.Rating = Convert.ToDouble(Rating);
               // _Feedback.Status = Status;
               // _Feedback.CreatedDateTime = Convert.ToDateTime(CreatedDate);
                _Feedback.CreatedBy = Convert.ToInt32(UserId);
                _db.Feedbacks.Add(_Feedback);
                _db.SaveChanges();
                _FeedbackResponse.Flag = "true";
                _FeedbackResponse.MESSAGE = "Feedback saved successfully";
            }


            return Request.CreateResponse(HttpStatusCode.OK, _FeedbackResponse);
        }
    }
}
