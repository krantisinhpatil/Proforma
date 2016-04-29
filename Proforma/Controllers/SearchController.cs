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
    public class SearchController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpGet]
        public HttpResponseMessage SearchCompanies(string keyword, long? UserId = 0)
        {
            SearchCompanyResponse _CompanyResponse = new SearchCompanyResponse();
            _CompanyResponse.MESSAGE = "Companies";
            _CompanyResponse.Flag = "false";
            var arrCompanyIds = new List<UserFavorite>();
            if (UserId > 0)
            {
                arrCompanyIds = _db.UserFavorites.Where(a => a.UserId == UserId).ToList();
            }

            var lstCompanies = _db.Companies.Where(m => m.CompanyName.ToLower().Contains(keyword) || m.Description.ToLower().Contains(keyword)).ToList();
            List<CompanySearchModel> lstCompanyVM = new List<CompanySearchModel>();
            if (lstCompanies != null)
            {
                _CompanyResponse.Flag = "true";



                for (int i = 0; i < lstCompanies.Count(); i++)
                {
                    CompanySearchModel _CompanyViewModel = new CompanySearchModel();
                    _CompanyViewModel.CompanyId = lstCompanies[i].CompanyId;
                    int companyid = lstCompanies[i].CompanyId;
                    List<CompanyCategories> lstCompCat = new List<CompanyCategories>();
                    var companycatategories = _db.CompanyCategories.Where(a => a.CompanyId == companyid).ToList();
                    if (null != companycatategories && companycatategories.Count() > 0)
                    {
                        foreach (var cat in companycatategories)
                        {
                            CompanyCategories _CompCat = new CompanyCategories();
                            var categoryname = _db.Categories.FirstOrDefault(c => c.CategoryId == cat.CategoryId);
                            _CompCat.CategoryId = cat.CategoryId;
                            _CompCat.CategoryName = categoryname.Category1;
                            lstCompCat.Add(_CompCat);
                        }
                    }
                    _CompanyViewModel.CompanyCategories = lstCompCat;
                    //_CompanyViewModel.CategoryId = lstCompanies[i].CategoryId;
                    _CompanyViewModel.CompanyName = lstCompanies[i].CompanyName;
                    _CompanyViewModel.PartnerType = lstCompanies[i].PartnerType;
                    _CompanyViewModel.StreetAddress = lstCompanies[i].StreetAddress;
                    _CompanyViewModel.City = lstCompanies[i].City;
                    _CompanyViewModel.ZipCode = lstCompanies[i].ZipCode;
                    _CompanyViewModel.State = lstCompanies[i].State;
                    _CompanyViewModel.Phone1 = lstCompanies[i].Phone1;
                    _CompanyViewModel.Phone2 = lstCompanies[i].Phone2;
                    _CompanyViewModel.Description = string.IsNullOrEmpty(lstCompanies[i].Description) ? "Description is not available." : lstCompanies[i].Description;
                    _CompanyViewModel.IsFavourite = false;
                    _CompanyViewModel.Latitude = null == lstCompanies[i].Latitude ? 0 : lstCompanies[i].Latitude;
                    _CompanyViewModel.Longitude = null == lstCompanies[i].Longitude ? 0 : lstCompanies[i].Longitude;
                    if (null != arrCompanyIds)
                    {
                        bool containsItem = arrCompanyIds.Any(item => item.CompanyId == lstCompanies[i].CompanyId);
                        if (containsItem)
                        {
                            _CompanyViewModel.IsFavourite = true;
                        }
                    }
                    lstCompanyVM.Add(_CompanyViewModel);
                }
            }
            _CompanyResponse.Companies = lstCompanyVM;
            return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
        }
    }
}
