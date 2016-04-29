using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class EventController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        //[ActionName("GetAllEvents")]
        public HttpResponseMessage GetAllEvents()
        {
            var _Events = _db.Events;
            EventsModel _EventsModel = new EventsModel();
            _EventsModel.MESSAGE = "All Events";
            _EventsModel.Flag = "false";
            List<EventDetailsModel> _lstEventDetailsModel = new List<EventDetailsModel>();
            if (null != _Events)
            {
                _EventsModel.Flag = "true";
               // List<EventDetailsModel> _lstEventDetailsModel = new List<EventDetailsModel>();
                foreach (var eve in _Events)
                {
                    EventDetailsModel _EventDetailsModel = new EventDetailsModel();

                    _EventDetailsModel.EventId = eve.EventId;
                    _EventDetailsModel.EventTitle = eve.EventTitle;
                    _EventDetailsModel.Image = eve.Image;
                    _EventDetailsModel.Description = "" + eve.Description;
                    _EventDetailsModel.EventSubHeading = "" + eve.EventSubHeading;
                    _EventDetailsModel.Status = eve.Status;
                    _EventDetailsModel.CreatedDate = eve.CreatedDate;
                    _EventDetailsModel.CreatedBy = eve.CreatedBy;                   
                    _lstEventDetailsModel.Add(_EventDetailsModel);

                }
               
            }
            _EventsModel.EventDetailsModel = _lstEventDetailsModel;
            return Request.CreateResponse(HttpStatusCode.OK, _EventsModel);
        }

        [HttpGet]
        //[ActionName("GetEventDetails")]
        public HttpResponseMessage GetEventDetails(long EventId)
        {
            EventDetailsViewModel _EventDetailsViewModel = new EventDetailsViewModel();
            _EventDetailsViewModel.MESSAGE = "Event Details";
            _EventDetailsViewModel.Flag = "false";
            List<EventDetailsModel> _lstEventDetailsModel = new List<EventDetailsModel>();
            var _Event = _db.Events.FirstOrDefault(e => e.EventId == EventId);
            if (null != _Event)
            {
                _EventDetailsViewModel.Flag = "true";
                EventDetailsModel _EventDetailsModel = new EventDetailsModel();
                _EventDetailsModel.EventId = _Event.EventId;
                _EventDetailsModel.EventTitle = _Event.EventTitle;
                _EventDetailsModel.Image = _Event.Image;
                _EventDetailsModel.Description = _Event.Description;
                _EventDetailsModel.Status = _Event.Status;
                _EventDetailsModel.CreatedDate = _Event.CreatedDate;
                _EventDetailsModel.CreatedBy = _Event.CreatedBy;
                _EventDetailsModel.EventSubHeading = "" + _Event.EventSubHeading;
                _lstEventDetailsModel.Add(_EventDetailsModel);

            }
            _EventDetailsViewModel.EventDetailsModel = _lstEventDetailsModel;
            return Request.CreateResponse(HttpStatusCode.OK, _EventDetailsViewModel);
        }
    }
}
