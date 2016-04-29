using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class FeedbackModel
    {
        public int FeedBackID { get; set; }
        public string FeedBackText { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public string Status { get; set; }
        public float? Rating { get; set; }
    }

    public class FeedbackResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }
}