using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Proforma.ViewModel;
using Proforma.DAL;

namespace Proforma.Controllers
{
    public class CategoryController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        public HttpResponseMessage GetAllCategories()
        {

            CategoryResponse objresponse = new CategoryResponse();

            var lstCategories = _db.Categories.Select(o => new { o.CategoryId, o.Category1 }).ToList();
            objresponse.MESSAGE = "All Categories";
            objresponse.Flag = "false";

            if (lstCategories != null)
            {
                List<CategoryViewModel> lstCategoryViewModel = new List<CategoryViewModel>();
                objresponse.Flag = "true";
                for (int i = 0; i < lstCategories.Count(); i++)
                {

                    CategoryViewModel _CategoryViewModel = new CategoryViewModel();
                    _CategoryViewModel.CategoryName = lstCategories[i].Category1;
                    _CategoryViewModel.CategoryId = lstCategories[i].CategoryId;

                    _CategoryViewModel.FilterCriterias = new List<FilterCriteriaViewModel>();
                    var categoryFilterCriterias = _db.FilterCriterias.Where(fc => fc.CategoryId == _CategoryViewModel.CategoryId).ToList();

                    FilterCriteriaViewModel _DefaultFilterCriteria = new FilterCriteriaViewModel();
                    _DefaultFilterCriteria.FilterCriteriaId = 0;
                    _DefaultFilterCriteria.CategoryId = _CategoryViewModel.CategoryId;
                    _DefaultFilterCriteria.CriteriaText = "All";
                    _CategoryViewModel.FilterCriterias.Add(_DefaultFilterCriteria);

                    if (null != categoryFilterCriterias)
                    {
                        foreach (var fc in categoryFilterCriterias)
                        {
                            FilterCriteriaViewModel _FilterCriteriaViewModel = new FilterCriteriaViewModel();
                            _FilterCriteriaViewModel.FilterCriteriaId = fc.FilterCriteriaId;
                            _FilterCriteriaViewModel.CategoryId = fc.CategoryId;
                            _FilterCriteriaViewModel.CriteriaText = fc.CriteriaText;
                            _CategoryViewModel.FilterCriterias.Add(_FilterCriteriaViewModel);
                        }

                    }
                    lstCategoryViewModel.Add(_CategoryViewModel);

                }
                objresponse.Categories = lstCategoryViewModel;
                return Request.CreateResponse(HttpStatusCode.OK, objresponse);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, objresponse);
            }
        }

    }

}
