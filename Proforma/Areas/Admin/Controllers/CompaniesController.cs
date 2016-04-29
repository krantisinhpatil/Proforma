using Proforma.DAL;
using Proforma.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Proforma.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, CompanyAdmin")]
    public class CompaniesController : Controller
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        // GET: Admin/Companies
        public ActionResult Index()
        {
            var companies = _db.Companies.ToList();
            List<CompaniesModel> lstCompanies = new List<CompaniesModel>();
            if (companies.Count() > 0)
            {
                foreach (var company in companies)
                {
                    CompaniesModel model = new CompaniesModel();
                    model.CompanyId = company.CompanyId;
                    model.PartnerType = company.PartnerType;
                    model.CompanyName = company.CompanyName;
                    model.ASI = company.ASI;
                    model.PPAI = company.PPAI;
                    model.PPPC = company.PPPC;
                    model.SAGE = company.SAGE;
                    model.UPIC = company.UPIC;
                    lstCompanies.Add(model);
                }

            }
            return View(lstCompanies);
        }

        public ActionResult Company(int companyId)
        {
            CompanyModel model = new CompanyModel();
            var company = _db.Companies.FirstOrDefault(a => a.CompanyId == companyId);
            if (null != company)
            {
                ContactInfoModel _ContactInfo = GetContactInfo(company);
                model.ContactInfo = _ContactInfo;

                ProductsCapabilityModel _ProductsCapabilityModel = GetProductCapabilities(company.CompanyId);
                model.ProductsCapability = _ProductsCapabilityModel;

                ProformaProgramModel _ProformaProgramModel = GetProFormaPrograms(company.CompanyId);
                model.ProformaProgram = _ProformaProgramModel;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult ProformaProgramsPartial(ProformaProgramModel model)
        {
            var record = new ProformaProgram();
            if (model.ProformaProgramId > 0)
            {
                record = _db.ProformaPrograms.FirstOrDefault(a => a.ProformaProgramId == model.ProformaProgramId);
            }

            record.Name = model.Name;
            record.CompanyId = model.CompanyId;


            if (model.ProformaProgramId == 0)
            {
                record.CreatedDate = DateTime.Now;
                _db.ProformaPrograms.Add(record);
            }
            else
            {
                record.ModifiedDate = DateTime.Now;
                _db.Entry(record).State = EntityState.Modified;
            }
            _db.SaveChanges();

            ProformaProgramModel _ProformaProgramModel = GetProFormaPrograms(model.CompanyId);
            return PartialView(_ProformaProgramModel);
        }

        [HttpPost]
        public ActionResult ProductCapabilitiesPartial(ProductsCapabilityModel model)
        {
            var record = new ProductsCapability();
            if (model.ProductsCapabilityId > 0)
            {
                record = _db.ProductsCapabilities.FirstOrDefault(a => a.ProductsCapabilityId == model.ProductsCapabilityId);
            }

            record.Name = model.Name;
            record.CompanyId = model.CompanyId;


            if (model.ProductsCapabilityId == 0)
            {
                record.CreatedDate = DateTime.Now;
                _db.ProductsCapabilities.Add(record);
            }
            else
            {
                record.ModifiedDate = DateTime.Now;
                _db.Entry(record).State = EntityState.Modified;
            }
            _db.SaveChanges();

            ProductsCapabilityModel _ProductsCapabilityModel = GetProductCapabilities(model.CompanyId);

            return PartialView(_ProductsCapabilityModel);
        }

        [HttpPost]
        public ActionResult ContactInfoPartial(ContactInfoModel model)
        {
            var company = _db.Companies.FirstOrDefault(a => a.CompanyId == model.CompanyId);
            ContactInfoModel _ContactInfo = new ContactInfoModel();
            if (null != company)
            {
                company.CompanyName = model.CompanyName;
                company.CompanyId = model.CompanyId;
                company.ASI = model.ASI;
                company.PPAI = model.PPAI;
                company.SAGE = model.SAGE;
                company.StreetAddress = model.StreetAddress;
                company.City = model.City;
                company.State = model.State;
                company.ZipCode = model.ZipCode;
                company.Website = model.Website;
                company.FOBPointInCanada = model.FOBPointInCanada;
                company.PrimaryContactFirstName = model.PrimaryContactFirstName + " " + model.PrimaryContactLastName;
                company.PrimaryContactEmail = model.PrimaryContactEmail;
                company.PrimaryContactDirectLine = model.PrimaryContactDirectLine;
                company.PrimaryContactFax = model.PrimaryContactFax;
                company.PrimaryContactTradeOnly = model.PrimaryContactTradeOnly;
                company.SecondaryContactFirstName = model.SecondaryContactFirstName + " " + model.SecondaryContactLastName;
                company.SecondaryContactEmail = model.SecondaryContactEmail;
                company.SecondaryContactDirectLine = model.SecondaryContactDirectLine;
                company.SecondaryContactFax = model.SecondaryContactFax;
                company.SecondaryContactTradeOnly = model.SecondaryContactTradeOnly;
                //  company.SecondaryContactAffilations = "";
                //company.PrimaryContactAffilations = "";
                _db.Entry(company).State = EntityState.Modified;
                _db.SaveChanges();

                //To Do: Edit company data
                _ContactInfo = GetContactInfo(company);
            }
            return PartialView(_ContactInfo);
        }

        public ContactInfoModel GetContactInfo(Company company)
        {
            ContactInfoModel _ContactInfo = new ContactInfoModel();
            _ContactInfo.CompanyName = company.CompanyName;
            _ContactInfo.CompanyId = company.CompanyId;
            _ContactInfo.ASI = company.ASI;
            _ContactInfo.PPAI = company.PPAI;
            _ContactInfo.SAGE = company.SAGE;
            _ContactInfo.StreetAddress = company.StreetAddress;
            _ContactInfo.City = company.City;
            _ContactInfo.State = company.State;
            _ContactInfo.ZipCode = company.ZipCode;
            _ContactInfo.Website = company.Website;
            _ContactInfo.FOBPointInCanada = company.FOBPointInCanada;
            _ContactInfo.PrimaryContactFirstName = company.PrimaryContactFirstName;
            _ContactInfo.PrimaryContactLastName = company.PrimaryContactLastName;
            _ContactInfo.PrimaryContactEmail = company.PrimaryContactEmail;
            _ContactInfo.PrimaryContactDirectLine = company.PrimaryContactDirectLine;
            _ContactInfo.PrimaryContactAffilations = company.PrimaryContactAffiliations;
            _ContactInfo.PrimaryContactFax = company.PrimaryContactFax;
            _ContactInfo.PrimaryContactTradeOnly = company.PrimaryContactTradeOnly;
            _ContactInfo.SecondaryContactFirstName = company.SecondaryContactFirstName;
            _ContactInfo.SecondaryContactLastName = company.SecondaryContactLastName;
            _ContactInfo.SecondaryContactEmail = company.SecondaryContactEmail;
            _ContactInfo.SecondaryContactDirectLine = company.SecondaryContactDirectLine;
            _ContactInfo.SecondaryContactAffilations = company.SecondaryContactAffiliations;
            _ContactInfo.SecondaryContactFax = company.SecondaryContactFax;
            _ContactInfo.SecondaryContactTradeOnly = company.SecondaryContactTradeOnly;
            _ContactInfo.TertiaryContactFirstName = company.TertiaryContactFirstName;
            _ContactInfo.TertiaryContactLastName = company.TertiaryContactLastName;
            _ContactInfo.TertiaryContactEmail = company.TertiaryContactEmail;
            _ContactInfo.TertiaryContactDirectLine = company.TertiaryContactDirectLine;
            _ContactInfo.TertiaryContactAffilations = company.TertiaryContactAffiliations;
            _ContactInfo.TertiaryContactFax = company.TertiaryContactFax;
            _ContactInfo.TertiaryContactTradeOnly = company.TertiaryContactTradeOnly;
            _ContactInfo.CompanyLogo = company.Company_Logo;
            return _ContactInfo;
        }

        public ProformaProgramModel GetProFormaPrograms(int companyId)
        {
            ProformaProgramModel _ProformaProgramModel = new ProformaProgramModel();
            _ProformaProgramModel.CompanyId = companyId;
            var _ProformaPrograms = _db.ProformaPrograms.Where(a => a.CompanyId == companyId).ToList();
            if (_ProformaPrograms.Count > 0)
            {
                List<ProformaProgram> lstProductsCapability = new List<ProformaProgram>();
                foreach (var item in _ProformaPrograms)
                {
                    ProformaProgram _ProformaProgram = new ProformaProgram();
                    _ProformaProgram.CompanyId = item.CompanyId;
                    _ProformaProgram.Name = item.Name;
                    _ProformaProgram.ProformaProgramId = item.ProformaProgramId;
                    lstProductsCapability.Add(_ProformaProgram);
                }
                _ProformaProgramModel.lstProformaProgram = lstProductsCapability;
            }
            return _ProformaProgramModel;
        }

        public ProductsCapabilityModel GetProductCapabilities(int companyId)
        {
            ProductsCapabilityModel _ProductsCapabilityModel = new ProductsCapabilityModel();
            _ProductsCapabilityModel.CompanyId = companyId;
            var _ProductsCapabilities = _db.ProductsCapabilities.Where(a => a.CompanyId == companyId).ToList();
            if (_ProductsCapabilities.Count > 0)
            {
                List<ProductsCapability> lstProductsCapability = new List<ProductsCapability>();
                foreach (var item in _ProductsCapabilities)
                {
                    ProductsCapability _ProductsCapability = new ProductsCapability();
                    _ProductsCapability.CompanyId = item.CompanyId;
                    _ProductsCapability.Name = item.Name;
                    _ProductsCapability.ProductsCapabilityId = item.ProductsCapabilityId;
                    lstProductsCapability.Add(_ProductsCapability);
                }
                _ProductsCapabilityModel.lstProductsCapabilities = lstProductsCapability;
            }
            return _ProductsCapabilityModel;
        }



        public JsonResult SaveContactInfoDetails(string ContactInfoData)
        {
            var serializer = new JavaScriptSerializer();
            var contactInfo = serializer.Deserialize(ContactInfoData, typeof(ContactInfoModel));
            ContactInfoModel model = (dynamic)(contactInfo);

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
            company.PrimaryContactFirstName = ""+model.PrimaryContactFirstName;
            company.PrimaryContactLastName =""+ model.PrimaryContactLastName;
            company.PrimaryContactEmail = "" + model.PrimaryContactEmail;
            company.PrimaryContactDirectLine = "" + model.PrimaryContactDirectLine;
            company.PrimaryContactFax = ""+model.PrimaryContactFax;
            company.PrimaryContactTradeOnly = "" + model.PrimaryContactTradeOnly;
            company.PrimaryContactAffiliations = ""+model.PrimaryContactAffilations;
            company.SecondaryContactFirstName =""+ model.SecondaryContactFirstName;
            company.SecondaryContactLastName = ""+model.SecondaryContactLastName;
            company.SecondaryContactEmail = ""+model.SecondaryContactEmail;
            company.SecondaryContactDirectLine = ""+model.SecondaryContactDirectLine;
            company.SecondaryContactFax = ""+model.SecondaryContactFax;
            company.SecondaryContactTradeOnly = ""+model.SecondaryContactTradeOnly;
            company.SecondaryContactAffiliations = ""+model.SecondaryContactAffilations;
            company.TertiaryContactFirstName = ""+model.TertiaryContactFirstName;
            company.TertiaryContactLastName =""+ model.TertiaryContactLastName;
            company.TertiaryContactEmail =""+ model.TertiaryContactEmail;
            company.TertiaryContactDirectLine =""+ model.TertiaryContactDirectLine;
            company.TertiaryContactFax = "" + model.TertiaryContactFax;
            company.TertiaryContactTradeOnly = "" + model.TertiaryContactTradeOnly;
            company.TertiaryContactAffiliations =""+ model.TertiaryContactAffilations;
            if(!string.IsNullOrEmpty(model.CompanyLogo))
            {
                company.Company_Logo = Path.Combine(Server.MapPath("~/Uploads/"), model.CompanyLogo);
            }
            else
            {
                company.Company_Logo = "";
            }
            

            _db.Entry(company).State = EntityState.Modified;
            _db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveProductCapability(string ProductCapabilityData, int ProductCapabilityId)
        {
            var serializer = new JavaScriptSerializer();
            var productCapability = serializer.Deserialize(ProductCapabilityData, typeof(ProductsCapabilityModel));
            ProductsCapabilityModel model = (dynamic)(productCapability);

            ProductsCapability product = new ProductsCapability();

            if(ProductCapabilityId>0)
            {
                product = _db.ProductsCapabilities.FirstOrDefault(a => a.ProductsCapabilityId == ProductCapabilityId);
                product.ModifiedDate = DateTime.Now;              
            }
            product.Name = model.Name;
            product.CompanyId = model.CompanyId;

            if (ProductCapabilityId > 0)
            {
                _db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                product.CreatedDate = DateTime.Now;
                _db.ProductsCapabilities.Add(product);
            }
            _db.SaveChanges();
            ProductNProgramsResponseModel response = new ProductNProgramsResponseModel();
            response.Message = "Success";
            response.Id = product.ProductsCapabilityId;
            response.Name = product.Name;

           return Json(response, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveProformaPrograms(string ProformaProgramsData, int ProformaProgramsId)
        {
            var serializer = new JavaScriptSerializer();
            var proformaProgram = serializer.Deserialize(ProformaProgramsData, typeof(ProformaProgramModel));
            ProformaProgramModel model = (dynamic)(proformaProgram);

            ProformaProgram program = new ProformaProgram();

            if (ProformaProgramsId > 0)
            {
                program = _db.ProformaPrograms.FirstOrDefault(a => a.ProformaProgramId == ProformaProgramsId);
                program.ModifiedDate = DateTime.Now;
            }
            program.Name = model.Name;
            program.CompanyId = model.CompanyId;

            if (ProformaProgramsId > 0)
            {
                _db.Entry(program).State = EntityState.Modified;
            }
            else
            {
                program.CreatedDate = DateTime.Now;
                _db.ProformaPrograms.Add(program);
            }
            _db.SaveChanges();

            ProductNProgramsResponseModel response = new ProductNProgramsResponseModel();
            response.Message = "Success";
            response.Id = program.ProformaProgramId;
            response.Name = program.Name;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductCapability(int ProductCapabilityId)
        {
            var record = _db.ProductsCapabilities.FirstOrDefault(a => a.ProductsCapabilityId == ProductCapabilityId);
            if (null != record)
            {
                _db.ProductsCapabilities.Remove(record);
                _db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProformaProgram(int ProformaProgramId)
        {
            var record = _db.ProformaPrograms.FirstOrDefault(a => a.ProformaProgramId == ProformaProgramId);
            if (null != record)
            {
                _db.ProformaPrograms.Remove(record);
                _db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            //HttpPostedFileBase file = Request.Files[0];
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object                    
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName); 

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
                        WebImage img = new WebImage(file.InputStream);
                       // if (img.Width > 1000)
                            img.Resize(65, 50);
                        img.Save(fname);
                        //file.SaveAs(fname);
                      //  return Json(fname);
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