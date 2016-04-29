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
    public class FilterCriteriaController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        //[ActionName("GetFilterCriteriaByCategory")]
        public HttpResponseMessage GetFilterCriteriaByCategory(int Id)
        {
            List<FilterCriteria> lstFilterCriteria = _db.FilterCriterias.Where(o => o.CategoryId == Id).ToList();

            FilterCriteriaResponse _FilterCriteriaResponse = new FilterCriteriaResponse();
            _FilterCriteriaResponse.MESSAGE = "Filter Criteria By Category";
            _FilterCriteriaResponse.Flag = "false";
            if (lstFilterCriteria != null)
            {
                List<FilterCriteriaViewModel> lstCriteria = new List<FilterCriteriaViewModel>();
                _FilterCriteriaResponse.Flag = "true";

                for (int i = 0; i < lstFilterCriteria.Count(); i++)
                {
                    FilterCriteriaViewModel _FilterCriteriaViewModel = new FilterCriteriaViewModel();
                    _FilterCriteriaViewModel.CategoryId = lstFilterCriteria[i].CategoryId;
                    _FilterCriteriaViewModel.FilterCriteriaId = lstFilterCriteria[i].FilterCriteriaId;
                    _FilterCriteriaViewModel.CriteriaText = lstFilterCriteria[i].CriteriaText;

                    lstCriteria.Add(_FilterCriteriaViewModel);
                }
                _FilterCriteriaResponse.Criteria = lstCriteria;

                return Request.CreateResponse(HttpStatusCode.OK, _FilterCriteriaResponse);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lstFilterCriteria);
        }
    }
}
