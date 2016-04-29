using Proforma.DAL;
using Proforma.Areas.Admin.ViewModels;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Proforma.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        // GET: Admin/SuperAdmin
        public ActionResult Index()
        {
            SuperAdminModel model = new SuperAdminModel();

            var categories = _db.Categories.Select(c => new { c.CategoryId, c.Category1 });
            ViewBag.Categories = new SelectList(categories.AsEnumerable(), "CategoryId", "Category1");

            ProformaPLPModel ProformaPLPs = new ProformaPLPModel();
            ProformaPLPs.PLPCompanies = GetCompanies();
            model.PLPs = ProformaPLPs;

            List<ProformaEventsModel> ProformaEvents = GetEvents();
            model.Events = ProformaEvents;

            ProformaOwnersModel ProformaOwners = new ProformaOwnersModel();
            model.Owners = ProformaOwners;

            ProformaAnalyticsModel ProformaAnalytics = new ProformaAnalyticsModel();
            model.Analytics = ProformaAnalytics;

            return View(model);
        }

        public List<PLPCompanySortedModel> GetCompanies(int? CategoryId=0)
        {
            List<PLPCompanySortedModel> lstPLPCompanies = new List<PLPCompanySortedModel>();
            lstPLPCompanies = GetAllCompanies();
            //var companies = _db.Companies.Where(a => a.Status != "Inactive").OrderByDescending(a => a.Status == "New").ToList();
           // lstPLPCompanies = GetPLPCompanyData(companies);
            return lstPLPCompanies;
        }

        public List<PLPCompanySortedModel> GetAllCompanies(string CompanyName = null, string PartnerType = null, string Status = null, string SortOrder = null, int? CategoryId = null)
        {
            var sortcolumn = "CompanyName";
            List<PLPCompanySortedModel> lstPLPCompanies = new List<PLPCompanySortedModel>();
            if(string.IsNullOrEmpty(CompanyName))
            {
                CompanyName = null;
            }
            if (string.IsNullOrEmpty(PartnerType))
            {
                PartnerType = null;
            }
            if (string.IsNullOrEmpty(Status))
            {
                Status = null;
            }
            if (string.IsNullOrEmpty(SortOrder))
            {
                SortOrder = null;
            }
            var sortedcompanies = _db.Database.SqlQuery<PLPCompanySortedModel>("exec Pro_GetCompanies @CategoryId={0},@CompanyName={1},@PartnerType={2},@Status={3},@PageNo={4},@PageSize={5},@SortColumn={6},@SortOrder={7}", CategoryId, CompanyName, PartnerType, Status, 1, 1000, sortcolumn, SortOrder).ToList();
            lstPLPCompanies = sortedcompanies.ToList();
            //foreach (var item in sortedcompanies)
            //{
            //    PLPCompanyModel model = new PLPCompanyModel();

            //    if (item.CategoryId > 0)
            //    {
            //        var category = _db.Categories.FirstOrDefault(a => a.CategoryId == item.CategoryId);
            //        model.CategoryName = category.Category1;
            //    }
            //    model.CompanyStatus = item.CompanyStatus;
            //    model.CompanyName = item.CompanyName;
            //    model.Description = "";
            //    model.CompanyId = item.CompanyId;
            //    lstPLPCompanies.Add(model);
            //}
            return lstPLPCompanies;
        }

        //public JsonResult GetCompaniesByCategoryId(int id)
        //{
        //    List<PLPCompanyModel> lstPLPCompanies = new List<PLPCompanyModel>();
        //    var companies = _db.Companies.Where(a => a.Status != "Inactive").OrderByDescending(a => a.Status == "New").ToList();
        //    lstPLPCompanies = GetPLPCompanyData(companies);
        //    return Json(lstPLPCompanies, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetCompaniesByCompanyName(string CompanyName)
        //{
        //    List<PLPCompanyModel> lstPLPCompanies = new List<PLPCompanyModel>();
        //    var companies = _db.Companies.Where(a => a.Status != "Inactive" && a.CompanyName.ToLower().Contains(CompanyName.ToLower())).OrderByDescending(a => a.Status == "New").ToList();
        //    lstPLPCompanies = GetPLPCompanyData(companies);
        //    return Json(lstPLPCompanies, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetSortedCompanies(string CompanyName=null,string PartnerType=null,string Status=null,string SortOrder=null, int? CategoryId = null)
        {
            //var sortcolumn = "CompanyName";
            List<PLPCompanySortedModel> lstPLPCompanies = new List<PLPCompanySortedModel>();

            // var sortedcompanies =  _db.Database.SqlQuery<PLPCompanySortedModel>("exec Pro_GetCompanies @CategoryId={0},@CompanyName={1},@PartnerType={2},@Status={3},@PageNo={4},@PageSize={5},@SortColumn={6},@SortOrder={7}", CategoryId, CompanyName, PartnerType, Status, 1, 1000, sortcolumn, SortOrder).ToList();

            //foreach (var item in sortedcompanies)
            //{
            //    PLPCompanyModel model = new PLPCompanyModel();
            //    if (item.CategoryId > 0)
            //    {
            //        var category = _db.Categories.FirstOrDefault(a => a.CategoryId == item.CategoryId);
            //        model.CategoryName = category.Category1;
            //    }
            //    model.CompanyStatus = item.CompanyStatus;
            //    model.CompanyName = item.CompanyName;
            //    model.Description = "";
            //    model.CompanyId = item.CompanyId;
            //    lstPLPCompanies.Add(model);
            //}

            lstPLPCompanies = GetAllCompanies(CompanyName, PartnerType, Status, SortOrder, CategoryId);

            return Json(lstPLPCompanies, JsonRequestBehavior.AllowGet);
        }

        //public List<PLPCompanyModel> GetPLPCompanyData(List<Company> companies)
        //{
        //    List<PLPCompanyModel> lstPLPCompanies = new List<PLPCompanyModel>();
        //    if (null != companies && companies.Count() > 0)
        //    {
        //        foreach (var item in companies)
        //        {
        //            PLPCompanyModel model = new PLPCompanyModel();
        //            //if (item.CategoryId1 > 0)
        //            //{
        //            //    var category = _db.Categories.FirstOrDefault(a => a.CategoryId == item.CategoryId);
        //            //    model.CategoryName = category.Category1;
        //            //}
        //            model.CompanyStatus = item.Status;
        //            model.CompanyName = item.CompanyName;
        //            model.Description = "";
        //            model.CompanyId = item.CompanyId;
        //            lstPLPCompanies.Add(model);
        //        }
        //    }
        //    return lstPLPCompanies;
        //}

        public JsonResult GetPLPCompanyInfo(int id)
        {
            PLPCompanyInfoModel model = new PLPCompanyInfoModel();
           
            if(id>0)
            {
                var _company = _db.Companies.FirstOrDefault(a => a.CompanyId == id);
                if(null!= _company)
                {
                    model.ASI = _company.ASI;
                   // model.CategoryId = _company.CategoryId;
                    model.City = _company.City;
                    model.CompanyId = _company.CompanyId;
                    model.CompanyLogo = _company.Company_Logo;
                    model.CompanyName = _company.CompanyName;
                    model.PartnerType = _company.PartnerType;
                    model.FOBPointInCanada = _company.FOBPointInCanada;
                    model.PPAI = _company.PPAI;
                    model.PrimaryContactAffilations = _company.PrimaryContactAffiliations;
                    model.PrimaryContactDirectLine = _company.PrimaryContactDirectLine;
                    model.PrimaryContactEmail = _company.PrimaryContactEmail;
                    model.PrimaryContactFax = _company.PrimaryContactFax;
                    model.PrimaryContactFirstName = _company.PrimaryContactFirstName;
                    model.PrimaryContactLastName = _company.PrimaryContactLastName;
                    model.PrimaryContactTradeOnly = _company.PrimaryContactTradeOnly;
                    model.SAGE = _company.SAGE;
                    model.SecondaryContactAffilations = _company.SecondaryContactAffiliations;
                    model.SecondaryContactDirectLine = _company.SecondaryContactDirectLine;
                    model.SecondaryContactEmail = _company.SecondaryContactEmail;
                    model.SecondaryContactFax = _company.SecondaryContactFax;
                    model.SecondaryContactFirstName = _company.SecondaryContactFirstName;
                    model.SecondaryContactLastName = _company.SecondaryContactLastName;
                    model.SecondaryContactTradeOnly = _company.SecondaryContactTradeOnly;
                    model.State = _company.State;
                    model.StreetAddress = _company.StreetAddress;
                    model.TertiaryContactAffilations = _company.TertiaryContactAffiliations;
                    model.TertiaryContactDirectLine = _company.TertiaryContactDirectLine;
                    model.TertiaryContactEmail = _company.TertiaryContactEmail;
                    model.TertiaryContactFax = _company.TertiaryContactFax;
                    model.TertiaryContactFirstName = _company.TertiaryContactFirstName;
                    model.TertiaryContactLastName = _company.TertiaryContactLastName;
                    model.TertiaryContactTradeOnly = _company.TertiaryContactTradeOnly;
                    model.Website = _company.Website;
                    model.ZipCode = _company.ZipCode;
                    model.Description = _company.Description;
                    model.lstAllCategories = new List<int>();
                    var categories = _db.CompanyCategories.Where(a => a.CompanyId == _company.CompanyId);
                    if(null!= categories && categories.Count()>0)
                    {
                        foreach(var cat in categories)
                        {
                            model.lstAllCategories.Add(cat.CategoryId);
                        }
                    }                  
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePLPCompanyInfo(string PLPCompanyInfoData)
        {
            
            string finalString = string.Empty;
            string original = PLPCompanyInfoData;

            if (!string.IsNullOrEmpty(PLPCompanyInfoData))
            {
                PLPCompanyInfoData = PLPCompanyInfoData.Replace('"', '\'');

                var arr11 = PLPCompanyInfoData.Split(new string[] { "'lstAllCategories':" }, StringSplitOptions.None);
                var arr12 = arr11[1].Split(new string[] { ",'PrimaryContactFirstName'" }, StringSplitOptions.None);
                var cat1 = arr12[0];

                if (!cat1.Contains("["))
                {
                    var arrNumberOnly = cat1.Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);
                    finalString = arr11[0] + "'lstAllCategories':" + "[" + arrNumberOnly[0] + "]" + ",'PrimaryContactFirstName'" + arr12[1];
                }
                else
                {
                    finalString = original;
                }
            }
            var serializer = new JavaScriptSerializer();
           
            var companyInfo = serializer.Deserialize(finalString, typeof(PLPCompanyInfoModel));
            PLPCompanyInfoModel model = (dynamic)(companyInfo);

            if (model.CompanyId > 0)
            {
                var company = _db.Companies.FirstOrDefault(a => a.CompanyId == model.CompanyId);

                company.CompanyName = model.CompanyName;
                company.ASI = "" + model.ASI;
                company.PPAI = "" + model.PPAI;
                company.SAGE = "" + model.SAGE;
                company.StreetAddress = "" + model.StreetAddress;
                company.City = "" + model.City;
                company.State = "" + model.State;
                company.ZipCode = "" + model.ZipCode;
                company.Website = "" + model.Website;
                company.FOBPointInCanada = "" + model.FOBPointInCanada;
                company.PrimaryContactFirstName = "" + model.PrimaryContactFirstName;
                company.PrimaryContactLastName = "" + model.PrimaryContactLastName;
                company.PrimaryContactEmail = "" + model.PrimaryContactEmail;
                company.PrimaryContactDirectLine = "" + model.PrimaryContactDirectLine;
                company.PrimaryContactFax = "" + model.PrimaryContactFax;
                company.PrimaryContactTradeOnly = "" + model.PrimaryContactTradeOnly;
                company.PrimaryContactAffiliations = "" + model.PrimaryContactAffilations;
                company.SecondaryContactFirstName = "" + model.SecondaryContactFirstName;
                company.SecondaryContactLastName = "" + model.SecondaryContactLastName;
                company.SecondaryContactEmail = "" + model.SecondaryContactEmail;
                company.SecondaryContactDirectLine = "" + model.SecondaryContactDirectLine;
                company.SecondaryContactFax = "" + model.SecondaryContactFax;
                company.SecondaryContactTradeOnly = "" + model.SecondaryContactTradeOnly;
                company.SecondaryContactAffiliations = "" + model.SecondaryContactAffilations;
                company.TertiaryContactFirstName = "" + model.TertiaryContactFirstName;
                company.TertiaryContactLastName = "" + model.TertiaryContactLastName;
                company.TertiaryContactEmail = "" + model.TertiaryContactEmail;
                company.TertiaryContactDirectLine = "" + model.TertiaryContactDirectLine;
                company.TertiaryContactFax = "" + model.TertiaryContactFax;
                company.TertiaryContactTradeOnly = "" + model.TertiaryContactTradeOnly;
                company.TertiaryContactAffiliations = "" + model.TertiaryContactAffilations;
                company.Description = "" + model.Description;

                if (null != model.lstAllCategories && model.lstAllCategories.Count() > 0)
                {
                    var companyCategories = _db.CompanyCategories.Where(cc => cc.CompanyId == company.CompanyId).ToList();
                    foreach (var comp in companyCategories)
                    {
                        _db.CompanyCategories.Remove(comp);
                    }
                    _db.SaveChanges();

                    foreach (var id in model.lstAllCategories)
                    {
                        CompanyCategory _CompanyCategory = new CompanyCategory();
                        _CompanyCategory.CompanyId = company.CompanyId;
                        _CompanyCategory.CategoryId = Convert.ToInt32(id);
                        _db.CompanyCategories.Add(_CompanyCategory);
                        _db.SaveChanges();
                    }
                }
                if (null == model.PartnerType)
                {
                    company.PartnerType = "PLP";
                }
                else
                {
                    company.PartnerType = "MVPLP";
                }
                if (!string.IsNullOrEmpty(model.CompanyLogo))
                {
                    company.Company_Logo = Path.Combine(Server.MapPath("~/Uploads/"), model.CompanyLogo);
                }
                else
                {
                    company.Company_Logo = "";
                }


                //_db.Entry(company).State = EntityState.Modified;
                //_db.SaveChanges();
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public List<ProformaEventsModel> GetEvents()
        {
            List<ProformaEventsModel> lstEvents = new List<ProformaEventsModel>();
            var AllEvents = _db.Events.ToList();
            if (null != AllEvents && AllEvents.Count() > 0)
            {
                foreach (var _event in AllEvents)
                {
                    ProformaEventsModel model = new ProformaEventsModel();
                    model.EventTitle = _event.EventTitle;
                    model.EventDescription = _event.Description;
                    model.ShortDescription = _event.EventSubHeading;
                    model.EventId = _event.EventId;
                    model.EventImage = _event.Image;
                    lstEvents.Add(model);
                }
            }
            return lstEvents;
        }

        [HttpPost]
        public JsonResult SaveEvent(string EventInfo)
        {
            var serializer = new JavaScriptSerializer();
            var eventInfo = serializer.Deserialize(EventInfo, typeof(ProformaEventsModel));
            ProformaEventsModel model = (dynamic)(eventInfo);
            Event _event = new Event();
            if (model.EventId>0)
            {
                _event = _db.Events.FirstOrDefault(a => a.EventId == model.EventId);
                _event.ModifiedDate = DateTime.Now;
            }
            else
            {
                _event.CreatedDate = DateTime.Now;
                _event.EventSubHeading = "";
            }
            
            _event.EventTitle = model.EventTitle;
            _event.Description = model.EventDescription;
            _event.EventSubHeading = model.ShortDescription;
            //_event.EventEndDate = model.EventEndDate;
            //_event.EventStartDate = model.EventStartDate;
            _event.Image = "http://psgapp.proforma.com//Uploads/" + model.EventImage;
            _event.Status = "Active";
            if (model.EventId > 0)
            {
                _db.Entry(_event).State = EntityState.Modified;
            }
            else
            {
                _db.Events.Add(_event);
            }
              
            _db.SaveChanges();
            ProformaEventResponseModel response = new ProformaEventResponseModel();
            response.EventId = _event.EventId;
            response.EventStartDate = _event.EventStartDate;
            response.EventEndDate = _event.EventEndDate;
            response.Description = _event.Description;
            response.EventTitle = _event.EventTitle;
            response.EventImage = _event.Image;

            response.Message = "Success";

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEvent(int id)
        {
            var record = _db.Events.FirstOrDefault(a => a.EventId == id);
            if (null != record)
            {
                _db.Events.Remove(record);
                _db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object                    
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);

                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

    }
}