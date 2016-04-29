using Proforma.DAL;
using Newtonsoft.Json;
using Proforma.Utils;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class CompanyController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        #region Company

        //api/Company/GetCompaniesByCategory?Id=1&FilterCriteriaId=2
        [HttpGet]
        public HttpResponseMessage GetCompaniesByCategory(int Id, int? FilterCriteriaId = 0, long? UserId = 0)
        {
            CompanyResponse _CompanyResponse = new CompanyResponse();
            _CompanyResponse.MESSAGE = "Companies By Category";
            _CompanyResponse.Flag = "false";

            List<Company> lstCompanies = new List<Company>();
            var companycategories = _db.CompanyCategories.Where(c => c.CategoryId == Id).Select(c => c.CompanyId);
            if (null != companycategories)
            {
                if (null == FilterCriteriaId || FilterCriteriaId < 1)
                {
                    foreach (var companyid in companycategories)
                    {
                        Company comp = _db.Companies.FirstOrDefault(a => a.CompanyId == companyid);
                        if (null != comp)
                        {
                            lstCompanies.Add(comp);
                        }

                    }
                    //lstCompanies = _db.Companies.Where(o => o.CategoryId == Id).ToList();

                }
                else
                {
                    lstCompanies = (from c in _db.Companies
                                    join cm in _db.CompanyMetas
                                    on c.CompanyId equals cm.CompanyId
                                    where cm.FilterCriteriaId == FilterCriteriaId && companycategories.Contains(c.CompanyId)
                                    select c).ToList();
                }

                var arrCompanyIds = new List<UserFavorite>();
                if (null != UserId)
                {
                    arrCompanyIds = _db.UserFavorites.Where(a => a.UserId == UserId).ToList();
                }

                if (lstCompanies != null)
                {
                    _CompanyResponse.Flag = "true";

                    List<CompanyViewModel> lstCompanyVM = new List<CompanyViewModel>();

                    for (int i = 0; i < lstCompanies.Count(); i++)
                    {
                        CompanyViewModel _CompanyViewModel = new CompanyViewModel();
                        _CompanyViewModel.CompanyId = lstCompanies[i].CompanyId;
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
                    _CompanyResponse.Companies = lstCompanyVM;

                    return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
        }

        [HttpGet]
        public HttpResponseMessage GetCompanyDetailsById(int Id)
        {
            var company = _db.Companies.FirstOrDefault(o => o.CompanyId == Id);
            CompanyDetailsResponse objCompanyDetails = new CompanyDetailsResponse();
            objCompanyDetails.MESSAGE = "Company Details";
            objCompanyDetails.Flag = "false";
            if (company != null)
            {
                objCompanyDetails.Flag = "true";
                var _Company = Common.GetCompanyDetails(company);
                _Company.IsFavourite = false;
                objCompanyDetails.CompanyDetails = _Company;
            }

            return Request.CreateResponse(HttpStatusCode.OK, objCompanyDetails);
        }

        [HttpGet]
        public HttpResponseMessage GetCompanyCoordinates()
        {
            var _Companies = _db.Companies.ToList();
            CompanyCoordinatesModel _CompanyCoordinatesModel = new CompanyCoordinatesModel();
            _CompanyCoordinatesModel.Flag = "false";
            _CompanyCoordinatesModel.MESSAGE = "Company Coordinates Info";
            if (null != _Companies && _Companies.Count() > 0)
            {
                List<ComapnyCoordinatesDetails> _lstComapnyCoordinates = new List<ComapnyCoordinatesDetails>();
                _CompanyCoordinatesModel.Flag = "true";
                foreach (var comp in _Companies)
                {
                    ComapnyCoordinatesDetails _ComapnyCoordinates = new ComapnyCoordinatesDetails();
                    _ComapnyCoordinates.CompanyId = comp.CompanyId;
                    _ComapnyCoordinates.CompanyName = comp.CompanyName;
                    _ComapnyCoordinates.PartnerType = comp.PartnerType;
                    _ComapnyCoordinates.StreetAddress = comp.StreetAddress;
                    _ComapnyCoordinates.City = comp.City;
                    _ComapnyCoordinates.State = comp.State;
                    _ComapnyCoordinates.ZipCode = comp.ZipCode;
                    _ComapnyCoordinates.Phone1 = comp.Phone1;
                    _ComapnyCoordinates.Phone2 = comp.Phone2;
                    _ComapnyCoordinates.Description = string.IsNullOrEmpty(comp.Description) ? "Description is not available." : comp.Description;
                    _ComapnyCoordinates.IsFavourite = false;
                    _ComapnyCoordinates.Latitude = null == comp.Latitude ? 0 : comp.Latitude;
                    _ComapnyCoordinates.Longitude = null == comp.Longitude ? 0 : comp.Longitude;
                    List<CompanyCategories> lstCompCat = new List<CompanyCategories>();
                    var companycatategories = _db.CompanyCategories.Where(a => a.CompanyId == comp.CompanyId).ToList();
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
                    _ComapnyCoordinates.CompanyCategories = lstCompCat;

                    _lstComapnyCoordinates.Add(_ComapnyCoordinates);
                }
                _CompanyCoordinatesModel.ComapnyCoordinates = _lstComapnyCoordinates;
            }
            return Request.CreateResponse(HttpStatusCode.OK, _CompanyCoordinatesModel);
        }

        #endregion

        #region MVPLPs

        [HttpGet]
        public HttpResponseMessage GetMVPLPCompanies(int? FilterCriteriaId = 0, long? UserId = 0)
        {
            MVPLPCompanyResponse _CompanyResponse = new MVPLPCompanyResponse();
            _CompanyResponse.MESSAGE = "MVPLP Companies";
            _CompanyResponse.Flag = "false";

            List<Company> lstCompanies;

            if (null == FilterCriteriaId || FilterCriteriaId < 1)
            {
                lstCompanies = _db.Companies.Where(o => o.PartnerType.ToLower() == "mvplp").ToList();
            }
            else
            {
                lstCompanies = (from c in _db.Companies
                                join cm in _db.CompanyCategories
                                on c.CompanyId equals cm.CompanyId
                                where cm.CategoryId == FilterCriteriaId && c.PartnerType.ToLower() == "mvplp"
                                select c).ToList();
            }

            var arrCompanyIds = new List<UserFavorite>();
            if (null != UserId)
            {
                arrCompanyIds = _db.UserFavorites.Where(a => a.UserId == UserId).ToList();
            }

            if (lstCompanies != null)
            {
                _CompanyResponse.Flag = "true";

                List<MVPLPCompanyViewModel> lstCompanyVM = new List<MVPLPCompanyViewModel>();

                for (int i = 0; i < lstCompanies.Count(); i++)
                {
                    var compId = lstCompanies[i].CompanyId;
                    MVPLPCompanyViewModel _CompanyViewModel = new MVPLPCompanyViewModel();
                    _CompanyViewModel.CompanyId = compId;
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
                        bool containsItem = arrCompanyIds.Any(item => item.CompanyId == compId);
                        if (containsItem)
                        {
                            _CompanyViewModel.IsFavourite = true;
                        }
                    }
                    /*
                    List<CompanyCategories> lstCompCat = new List<CompanyCategories>();

                    var companycatategories = _db.CompanyCategories.Where(a => a.CompanyId == compId).ToList();

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
                    */
                    int catId = 0;
                    var companyCategory = _db.CompanyCategories.FirstOrDefault(cc => cc.CompanyId == compId);
                    if (null != companyCategory)
                    {
                        catId = companyCategory.CategoryId;
                    }
                    _CompanyViewModel.CategoryId = catId;

                    lstCompanyVM.Add(_CompanyViewModel);
                }
                _CompanyResponse.Companies = lstCompanyVM;

                return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
        }

        #endregion

        #region ProExclusives

        [HttpGet]
        public HttpResponseMessage GetProExclusives()
        {
            ProExclusiveResponse _Response = new ProExclusiveResponse();
            _Response.MESSAGE = "ProExclusives";
            _Response.Flag = "true";

            try
            {
                var proExclusives = _db.ProExclusives.Where(p => p.ValidTill >= DateTime.Now);
                foreach (var proExclusive in proExclusives)
                {
                    _Response.ProExclusives.Add(Common.GetProExclusiveVM(proExclusive));
                }
            }
            catch (Exception)
            {
                _Response.Flag = "false";
            }

            return Request.CreateResponse(HttpStatusCode.OK, _Response);
        }

        #endregion
    }
}
