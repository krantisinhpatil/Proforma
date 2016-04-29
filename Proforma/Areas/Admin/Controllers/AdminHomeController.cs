using Proforma.DAL;
using Newtonsoft.Json;
using OfficeOpenXml;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace Proforma.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult ImportExcel()
        {
            SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

            //string filePath = Server.MapPath("/Docs/2016MASTERSourceGuideListings.xlsx");
            string filePath = "E:/Gotcha/Docs/2016MASTERSourceGuideListings.xlsx";
            var excelFile = new FileInfo(filePath);

            List<Company> CompanyList = new List<Company>();
            const int startRow = 1;
            try
            {
                using (var package = new ExcelPackage(excelFile))
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            // Get the worksheet
                            foreach (ExcelWorksheet currentWorksheet in workBook.Worksheets)
                            {
                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                                {
                                    var categoryText = currentWorksheet.Name;

                                    string category = Convert.ToString(currentWorksheet.Cells[rowNumber, 1].Value).Trim();
                                    if (!string.IsNullOrEmpty(category))
                                    {
                                        int? categoryId = null;
                                        var cat = _db.Categories.FirstOrDefault(c => c.Category1.ToLower() == categoryText.ToLower());
                                        if (null != cat)
                                        {
                                            categoryId = cat.CategoryId;
                                        }

                                        string MVPLP_PLP = Convert.ToString(currentWorksheet.Cells[rowNumber, 2].Value);
                                        string Company_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 3].Value);
                                        string Street_Address = Convert.ToString(currentWorksheet.Cells[rowNumber, 4].Value);
                                        string City = Convert.ToString(currentWorksheet.Cells[rowNumber, 5].Value);
                                        string State = Convert.ToString(currentWorksheet.Cells[rowNumber, 6].Value);
                                        string ZipCode = Convert.ToString(currentWorksheet.Cells[rowNumber, 7].Value);
                                        string Phone_1 = Convert.ToString(currentWorksheet.Cells[rowNumber, 8].Value);
                                        string Phone_2 = Convert.ToString(currentWorksheet.Cells[rowNumber, 9].Value);
                                        string Fax = Convert.ToString(currentWorksheet.Cells[rowNumber, 10].Value);
                                        string Website = Convert.ToString(currentWorksheet.Cells[rowNumber, 11].Value);
                                        string FTPSite = Convert.ToString(currentWorksheet.Cells[rowNumber, 12].Value);
                                        string ArtMail = Convert.ToString(currentWorksheet.Cells[rowNumber, 13].Value);

                                        string OrderEmail = Convert.ToString(currentWorksheet.Cells[rowNumber, 14].Value);
                                        string FOB_pointinCanada = Convert.ToString(currentWorksheet.Cells[rowNumber, 15].Value);
                                        string CanadianDollers = Convert.ToString(currentWorksheet.Cells[rowNumber, 16].Value);
                                        string PrimaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 17].Value);
                                        string PrimaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 18].Value);
                                        string PrimaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 19].Value);
                                        string PrimaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 20].Value);
                                        string SecondaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 21].Value);
                                        string SecondaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 22].Value);
                                        string SecondaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 23].Value);
                                        string SecondaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 24].Value);
                                        string TradeOnly = Convert.ToString(currentWorksheet.Cells[rowNumber, 25].Value);
                                        string Union = Convert.ToString(currentWorksheet.Cells[rowNumber, 26].Value);

                                        string ASI = Convert.ToString(currentWorksheet.Cells[rowNumber, 27].Value);
                                        string PPAI = Convert.ToString(currentWorksheet.Cells[rowNumber, 28].Value);
                                        string PPPC = Convert.ToString(currentWorksheet.Cells[rowNumber, 29].Value);
                                        string SAGE = Convert.ToString(currentWorksheet.Cells[rowNumber, 30].Value);
                                        string UPIC = Convert.ToString(currentWorksheet.Cells[rowNumber, 31].Value);
                                        string Certification = Convert.ToString(currentWorksheet.Cells[rowNumber, 32].Value);
                                        string Minority_Owned = Convert.ToString(currentWorksheet.Cells[rowNumber, 33].Value);
                                        string Proforma_Pricing = Convert.ToString(currentWorksheet.Cells[rowNumber, 34].Value);
                                        string Proforma_Programs = Convert.ToString(currentWorksheet.Cells[rowNumber, 35].Value);
                                        string Product_capability = Convert.ToString(currentWorksheet.Cells[rowNumber, 36].Value);
                                        string Rushor24hours = Convert.ToString(currentWorksheet.Cells[rowNumber, 37].Value);

                                        Company _objCompany = new Company { CategoryId1 = categoryId, PartnerType = MVPLP_PLP, CompanyName = Company_Name, StreetAddress = Street_Address, City = City, State = State, ZipCode = ZipCode, Phone1 = Phone_1, Phone2 = Phone_2, PrimaryContactFax = Fax, Website = Website, FTPSite = FTPSite, ArtEmail = ArtMail, OrderEmail = OrderEmail, FOBPointInCanada = FOB_pointinCanada, QuoteinCanadianDollars = CanadianDollers, PrimaryContactFirstName = PrimaryContact_Name, PrimaryContactEmail = PrimaryContact_Email, PrimaryContactExtension = PrimaryContact_Extension, PrimaryContactDirectLine = PrimaryContact_DirectLine, SecondaryContactFirstName = SecondaryContact_Name, SecondaryContactEmail = SecondaryContact_Email, SecondaryContactExtension = SecondaryContact_Extension, SecondaryContactDirectLine = SecondaryContact_DirectLine, PrimaryContactTradeOnly = TradeOnly, Union = Union, ASI = ASI, PPAI = PPAI, PPPC = PPPC, SAGE = SAGE, UPIC = UPIC, Certifications = Certification, MinorityOwned = Minority_Owned, ProformaPricing = Proforma_Pricing, ProformaPrograms = Proforma_Programs, ProductsNCapabilities = Product_capability, Rushor24hour = Rushor24hours };

                                        _db.Companies.Add(_objCompany);
                                    }
                                }
                            }
                        }
                    }
                }

                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
               .SelectMany(x => x.ValidationErrors)
               .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return View();
        }

        public ActionResult ImportFilterExcel()
        {
            SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

            //string filePath = Server.MapPath("/Docs/2016MASTERSourceGuideListings.xlsx");
            string filePath = "E:/Gotcha/Docs/2016MASTERSourceGuideListings.xlsx";
            var excelFile = new FileInfo(filePath);

            List<Company> CompanyList = new List<Company>();
            const int startRow = 1;
            try
            {
                using (var package = new ExcelPackage(excelFile))
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            // Get the worksheet
                            foreach (ExcelWorksheet currentWorksheet in workBook.Worksheets)
                            {
                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                                {

                                    string Company_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 3].Value);
                                    string City = Convert.ToString(currentWorksheet.Cells[rowNumber, 5].Value);
                                    var categoryText = currentWorksheet.Name;

                                    //Get categoryid
                                    var categories = Convert.ToString(currentWorksheet.Cells[rowNumber, 1].Value).Trim().Split(',');
                                    foreach (string category in categories)
                                    {
                                        if (!string.IsNullOrEmpty(category))
                                        {
                                            int? categoryId = null;
                                            if (!string.IsNullOrEmpty(category))
                                            {
                                                var cat = _db.Categories.FirstOrDefault(c => c.Category1.Trim().ToLower() == category.Trim().ToLower());
                                                if (null != cat)
                                                {
                                                    categoryId = cat.CategoryId;
                                                }
                                            }

                                            //int? FilterCriteriaId = null;
                                            var FiltercriteriaRecord = _db.FilterCriterias.FirstOrDefault(b => b.CategoryId == categoryId && b.CriteriaText.Trim().ToLower() == categoryText.Trim().ToLower());

                                            //Check if Company is already added in database
                                            var CompanyExists = _db.Companies.Where(a => a.CompanyName.Trim().ToLower() == Company_Name.Trim().ToLower() && a.City.Trim().ToLower() == City.Trim().ToLower() && a.CategoryId1 == categoryId).ToList();

                                            if (CompanyExists.Count() > 0)
                                            {
                                                foreach (var company in CompanyExists)
                                                {
                                                    CompanyMeta _CompanyMeta = new CompanyMeta();
                                                    _CompanyMeta.CompanyId = company.CompanyId;
                                                    _CompanyMeta.FilterCriteriaId = FiltercriteriaRecord.FilterCriteriaId;
                                                    _db.CompanyMetas.Add(_CompanyMeta);
                                                    _db.SaveChanges();
                                                }
                                            }
                                            else
                                            {
                                                string MVPLP_PLP = Convert.ToString(currentWorksheet.Cells[rowNumber, 2].Value);
                                                //string Company_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 3].Value);
                                                string Street_Address = Convert.ToString(currentWorksheet.Cells[rowNumber, 4].Value);
                                                //string City = Convert.ToString(currentWorksheet.Cells[rowNumber, 5].Value);
                                                string State = Convert.ToString(currentWorksheet.Cells[rowNumber, 6].Value);
                                                string ZipCode = Convert.ToString(currentWorksheet.Cells[rowNumber, 7].Value);
                                                string Phone_1 = Convert.ToString(currentWorksheet.Cells[rowNumber, 8].Value);
                                                string Phone_2 = Convert.ToString(currentWorksheet.Cells[rowNumber, 9].Value);
                                                string Fax = Convert.ToString(currentWorksheet.Cells[rowNumber, 10].Value);
                                                string Website = Convert.ToString(currentWorksheet.Cells[rowNumber, 11].Value);
                                                string FTPSite = Convert.ToString(currentWorksheet.Cells[rowNumber, 12].Value);
                                                string ArtMail = Convert.ToString(currentWorksheet.Cells[rowNumber, 13].Value);

                                                string OrderEmail = Convert.ToString(currentWorksheet.Cells[rowNumber, 14].Value);
                                                string FOB_pointinCanada = Convert.ToString(currentWorksheet.Cells[rowNumber, 15].Value);
                                                string CanadianDollers = Convert.ToString(currentWorksheet.Cells[rowNumber, 16].Value);
                                                string PrimaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 17].Value);
                                                string PrimaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 18].Value);
                                                string PrimaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 19].Value);
                                                string PrimaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 20].Value);
                                                string SecondaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 21].Value);
                                                string SecondaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 22].Value);
                                                string SecondaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 23].Value);
                                                string SecondaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 24].Value);
                                                string TradeOnly = Convert.ToString(currentWorksheet.Cells[rowNumber, 25].Value);
                                                string Union = Convert.ToString(currentWorksheet.Cells[rowNumber, 26].Value);

                                                string ASI = Convert.ToString(currentWorksheet.Cells[rowNumber, 27].Value);
                                                string PPAI = Convert.ToString(currentWorksheet.Cells[rowNumber, 28].Value);
                                                string PPPC = Convert.ToString(currentWorksheet.Cells[rowNumber, 29].Value);
                                                string SAGE = Convert.ToString(currentWorksheet.Cells[rowNumber, 30].Value);
                                                string UPIC = Convert.ToString(currentWorksheet.Cells[rowNumber, 31].Value);
                                                string Certification = Convert.ToString(currentWorksheet.Cells[rowNumber, 32].Value);
                                                string Minority_Owned = Convert.ToString(currentWorksheet.Cells[rowNumber, 33].Value);
                                                string Proforma_Pricing = Convert.ToString(currentWorksheet.Cells[rowNumber, 34].Value);
                                                string Proforma_Programs = Convert.ToString(currentWorksheet.Cells[rowNumber, 35].Value);
                                                string Product_capability = Convert.ToString(currentWorksheet.Cells[rowNumber, 36].Value);
                                                string Rushor24hours = Convert.ToString(currentWorksheet.Cells[rowNumber, 37].Value);

                                                Company _objCompany = new Company { CategoryId1 = categoryId, PartnerType = MVPLP_PLP, CompanyName = Company_Name, StreetAddress = Street_Address, City = City, State = State, ZipCode = ZipCode, Phone1 = Phone_1, Phone2 = Phone_2, PrimaryContactFax = Fax, Website = Website, FTPSite = FTPSite, ArtEmail = ArtMail, OrderEmail = OrderEmail, FOBPointInCanada = FOB_pointinCanada, QuoteinCanadianDollars = CanadianDollers, PrimaryContactFirstName = PrimaryContact_Name, PrimaryContactEmail = PrimaryContact_Email, PrimaryContactExtension = PrimaryContact_Extension, PrimaryContactDirectLine = PrimaryContact_DirectLine, SecondaryContactFirstName = SecondaryContact_Name, SecondaryContactEmail = SecondaryContact_Email, SecondaryContactExtension = SecondaryContact_Extension, SecondaryContactDirectLine = SecondaryContact_DirectLine, PrimaryContactTradeOnly = TradeOnly, Union = Union, ASI = ASI, PPAI = PPAI, PPPC = PPPC, SAGE = SAGE, UPIC = UPIC, Certifications = Certification, MinorityOwned = Minority_Owned, ProformaPricing = Proforma_Pricing, ProformaPrograms = Proforma_Programs, ProductsNCapabilities = Product_capability, Rushor24hour = Rushor24hours };

                                                _db.Companies.Add(_objCompany);
                                                _db.SaveChanges();

                                                CompanyMeta _CompanyMeta = new CompanyMeta();
                                                _CompanyMeta.CompanyId = _objCompany.CompanyId;
                                                _CompanyMeta.FilterCriteriaId = FiltercriteriaRecord.FilterCriteriaId;
                                                _db.CompanyMetas.Add(_CompanyMeta);
                                                _db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
               .SelectMany(x => x.ValidationErrors)
               .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return View();
        }

        public ActionResult UpdateCompanyInfo()
        {
            var _Companies = _db.Companies.ToList();
            if (_Companies != null || _Companies.Count() > 0)
            {
                foreach (var com in _Companies)
                {
                    var _Company = _db.Companies.FirstOrDefault(a => a.CompanyId == com.CompanyId);
                    string address = com.StreetAddress.Trim() + "+" + com.City.Trim() + "+" + com.ZipCode.Trim() + "+" + com.State.Trim();
                    var latlng = GetLatitudeLongitute(address);
                    if (!string.IsNullOrEmpty(latlng))
                    {
                        var arrlatlng = latlng.Split(new[] { '_' }).ToList();
                        _Company.Latitude = Convert.ToDecimal(arrlatlng[0]);
                        _Company.Longitude = Convert.ToDecimal(arrlatlng[1]);
                    }
                    _db.Entry(_Company).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return View();
        }

        public string GetLatitudeLongitute(string address)
        {
            using (WebClient _WebClient = new WebClient())
            {
                var result = _WebClient.DownloadData("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyAclUjgSa9XOqSoyZ8e_-z-j6-HPFyW6Zc");

                var kresult = Deserialize<RootObject>(result);
                if (kresult.results.Count > 0)
                {
                    var lat = kresult.results[0].geometry.location.lat;
                    var lng = kresult.results[0].geometry.location.lng;
                    return lat + "_" + lng;
                }
                else
                {
                    return "";
                }

            }
        }

        public ActionResult CompanyProgramsNProducts()
        {
            string[] stringSeparators = new string[] { ",", ". ", "\n", ";" };

            SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
            var companies = _db.Companies.ToList();
            foreach (var comp in companies)
            {
                //Manage Proforma Programs
                var proformaProgram = comp.ProformaPrograms;

                if (!string.IsNullOrEmpty(proformaProgram))
                {
                    var lstProformaPrograms = proformaProgram.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries); // proformaProgram.Split(new[] { ",", ". ", "\n", ";" }).ToList();
                    foreach (var pp in lstProformaPrograms)
                    {
                        var strProformaProgram = pp.Trim();
                        if (!string.IsNullOrEmpty(strProformaProgram))
                        {
                            var _ProformaProgram = _db.ProformaPrograms.FirstOrDefault(p => p.CompanyId == comp.CompanyId && p.Name == strProformaProgram);
                            if (null == _ProformaProgram)
                            {
                                _ProformaProgram = new ProformaProgram();
                                _ProformaProgram.CompanyId = comp.CompanyId;
                                _ProformaProgram.Name = strProformaProgram;
                                _ProformaProgram.CreatedDate = DateTime.Now;
                                _db.ProformaPrograms.Add(_ProformaProgram);
                            }
                            else
                            {
                                _ProformaProgram.Name = strProformaProgram;
                                _ProformaProgram.ModifiedDate = DateTime.Now;
                                _db.Entry(_ProformaProgram).State = EntityState.Modified;
                            }
                        }
                    }
                }

                //Manage ProductsCapabilities
                var productsCapability = comp.ProductsNCapabilities;
                if (!string.IsNullOrEmpty(productsCapability))
                {
                    var lstProductsCapabilities = productsCapability.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries); //productsCapability.Split(new[] { ',', '. ', '\n', ';' }).ToList();
                    foreach (var pc in lstProductsCapabilities)
                    {
                        var strProductsCapability = pc.Trim();
                        if (!string.IsNullOrEmpty(strProductsCapability))
                        {
                            var _ProductsCapability = _db.ProductsCapabilities.FirstOrDefault(p => p.CompanyId == comp.CompanyId && p.Name == strProductsCapability);
                            if (null == _ProductsCapability)
                            {
                                _ProductsCapability = new ProductsCapability();
                                _ProductsCapability.CompanyId = comp.CompanyId;
                                _ProductsCapability.Name = strProductsCapability;
                                _ProductsCapability.CreatedDate = DateTime.Now;
                                _db.ProductsCapabilities.Add(_ProductsCapability);
                            }
                            else
                            {
                                _ProductsCapability.Name = strProductsCapability;
                                _ProductsCapability.ModifiedDate = DateTime.Now;
                                _db.Entry(_ProductsCapability).State = EntityState.Modified;
                            }

                        }
                    }
                }

                _db.SaveChanges();
            }

            ViewBag.OperationStatus = "ProformaPrograms/ProductsCapabilities updated successfully!";
            return View();
        }

        public static T Deserialize<T>(byte[] data) where T : class
        {
            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream))
                return JsonSerializer.Create().Deserialize(reader, typeof(T)) as T;
        }


        public ActionResult ImportExcelMySql()
        {
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;database=proformanew;";
            //string filePath = Server.MapPath("/Docs/2016MASTERSourceGuideListings.xlsx");
            string filePath = "E:/Gotcha/Docs/21April2016/2016MASTERSourceGuideListings.xlsx";
            var excelFile = new FileInfo(filePath);

            List<Company> CompanyList = new List<Company>();
            const int startRow = 1;
            try
            {
                using (var package = new ExcelPackage(excelFile))
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            // Get the worksheet
                            foreach (ExcelWorksheet currentWorksheet in workBook.Worksheets)
                            {
                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                                {
                                    var categoryText = currentWorksheet.Name;

                                    string category = Convert.ToString(currentWorksheet.Cells[rowNumber, 1].Value).Trim();
                                    if (!string.IsNullOrEmpty(category))
                                    {
                                        //int? categoryId = null;
                                        //var cat = _db.Categories.FirstOrDefault(c => c.Category1.ToLower() == categoryText.ToLower());
                                        //if (null != cat)
                                        //{
                                        //    categoryId = cat.CategoryId;
                                        //}
                                        //string category = Convert.ToString(currentWorksheet.Cells[rowNumber, 1].Value).Trim();
                                        string MVPLP_PLP = Convert.ToString(currentWorksheet.Cells[rowNumber, 2].Value);
                                        string Company_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 3].Value);
                                        string Street_Address = Convert.ToString(currentWorksheet.Cells[rowNumber, 4].Value);
                                        string City = Convert.ToString(currentWorksheet.Cells[rowNumber, 5].Value);
                                        string State = Convert.ToString(currentWorksheet.Cells[rowNumber, 6].Value);
                                        string ZipCode = Convert.ToString(currentWorksheet.Cells[rowNumber, 7].Value);
                                        string Phone_1 = Convert.ToString(currentWorksheet.Cells[rowNumber, 8].Value);
                                        string Phone_2 = Convert.ToString(currentWorksheet.Cells[rowNumber, 9].Value);
                                        string Fax = Convert.ToString(currentWorksheet.Cells[rowNumber, 10].Value);
                                        string Website = Convert.ToString(currentWorksheet.Cells[rowNumber, 11].Value);
                                        string FTPSite = Convert.ToString(currentWorksheet.Cells[rowNumber, 12].Value);
                                        string ArtMail = Convert.ToString(currentWorksheet.Cells[rowNumber, 13].Value);

                                        string OrderEmail = Convert.ToString(currentWorksheet.Cells[rowNumber, 14].Value);
                                        string FOB_pointinCanada = Convert.ToString(currentWorksheet.Cells[rowNumber, 15].Value);
                                        string CanadianDollers = Convert.ToString(currentWorksheet.Cells[rowNumber, 16].Value);
                                        string PrimaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 17].Value);
                                        string PrimaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 18].Value);
                                        string PrimaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 19].Value);
                                        string PrimaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 20].Value);
                                        string SecondaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 21].Value);
                                        string SecondaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 22].Value);
                                        string SecondaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 23].Value);
                                        string SecondaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 24].Value);
                                        string TradeOnly = Convert.ToString(currentWorksheet.Cells[rowNumber, 25].Value);
                                        string Union = Convert.ToString(currentWorksheet.Cells[rowNumber, 26].Value);

                                        string ASI = Convert.ToString(currentWorksheet.Cells[rowNumber, 27].Value);
                                        string PPAI = Convert.ToString(currentWorksheet.Cells[rowNumber, 28].Value);
                                        string PPPC = Convert.ToString(currentWorksheet.Cells[rowNumber, 29].Value);
                                        string SAGE = Convert.ToString(currentWorksheet.Cells[rowNumber, 30].Value);
                                        string UPIC = Convert.ToString(currentWorksheet.Cells[rowNumber, 31].Value);
                                        string Certification = Convert.ToString(currentWorksheet.Cells[rowNumber, 32].Value);
                                        string Minority_Owned = Convert.ToString(currentWorksheet.Cells[rowNumber, 33].Value);
                                        string Proforma_Pricing = Convert.ToString(currentWorksheet.Cells[rowNumber, 34].Value);
                                        string Proforma_Programs = Convert.ToString(currentWorksheet.Cells[rowNumber, 35].Value);
                                        string Product_capability = Convert.ToString(currentWorksheet.Cells[rowNumber, 36].Value);
                                        string Rushor24hours = Convert.ToString(currentWorksheet.Cells[rowNumber, 37].Value);

                                    //    Company _objCompany = new Company { CategoryId = categoryId, PartnerType = MVPLP_PLP, CompanyName = Company_Name, StreetAddress = Street_Address, City = City, State = State, ZipCode = ZipCode, Phone1 = Phone_1, Phone2 = Phone_2, PrimaryContactFax = Fax, Website = Website, FTPSite = FTPSite, ArtEmail = ArtMail, OrderEmail = OrderEmail, FOBPointInCanada = FOB_pointinCanada, QuoteinCanadianDollars = CanadianDollers, PrimaryContactFirstName = PrimaryContact_Name, PrimaryContactEmail = PrimaryContact_Email, PrimaryContactExtension = PrimaryContact_Extension, PrimaryContactDirectLine = PrimaryContact_DirectLine, SecondaryContactFirstName = SecondaryContact_Name, SecondaryContactEmail = SecondaryContact_Email, SecondaryContactExtension = SecondaryContact_Extension, SecondaryContactDirectLine = SecondaryContact_DirectLine, PrimaryContactTradeOnly = TradeOnly, Union = Union, ASI = ASI, PPAI = PPAI, PPPC = PPPC, SAGE = SAGE, UPIC = UPIC, Certifications = Certification, MinorityOwned = Minority_Owned, ProformaPricing = Proforma_Pricing, ProformaPrograms = Proforma_Programs, ProductsNCapabilities = Product_capability, Rushor24hour = Rushor24hours };

                                        using (MySqlConnection con = new MySqlConnection(myConnectionString))
                                        {
                                           if(con.State==System.Data.ConnectionState.Closed)
                                            {
                                                con.Open();
                                            }
                                           string query = "INSERT INTO pro_company(CategoryId, PartnerType, CompanyName, StreetAddress, City, State, ZipCode, Phone1, Phone2, PrimaryContactFax, Website, FTPSite, ArtEmail, OrderEmail, FOBPointInCanada, QuoteinCanadianDollars, PrimaryContactFirstName, PrimaryContactEmail, PrimaryContactExtension, PrimaryContactDirectLine, SecondaryContactFirstName, SecondaryContactEmail, SecondaryContactExtension, SecondaryContactDirectLine, PrimaryContactTradeOnly, Union1, ASI, PPAI, PPPC, SAGE, UPIC, Certifications, MinorityOwned, ProformaPricing, ProformaPrograms, ProductsNCapabilities, Rushor24hour, Description, Latitude, Longitude, PrimaryContactLastName, PrimaryContactAffiliations, SecondaryContactLastName, SecondaryContactFax, SecondaryContactAffiliations, SecondaryContactTradeOnly, TertiaryContactFirstName, TertiaryContactLastName, TertiaryContactEmail, TertiaryContactDirectLine, TertiaryContactFax, TertiaryContactAffiliations, TertiaryContactTradeOnly, Company_Logo) VALUES('"+ category + "','" + MVPLP_PLP + "','" + Company_Name + "','" + Street_Address + "','" + City + "','" + State + "','" + ZipCode + "','" + Phone_1 + "','" + Phone_2 + "','" + Fax + "','" + Website + "','" + FTPSite + "','" + ArtMail + "','" + OrderEmail + "','" + FOB_pointinCanada + "','" + CanadianDollers + "','" + PrimaryContact_Name + "','" + PrimaryContact_Email + "','" + PrimaryContact_Extension + "','" + PrimaryContact_DirectLine + "','" + SecondaryContact_Name + "','" + SecondaryContact_Email + "','" + SecondaryContact_Extension + "','" + SecondaryContact_DirectLine + "','" + TradeOnly + "','" + Union + "','" + ASI + "','" + PPAI + "','" + PPPC + "','" + SAGE + "','" + UPIC + "','" + Certification + "','" + Minority_Owned + "','" + Proforma_Pricing + "','" + Proforma_Programs + "','" + Product_capability + "','" + Rushor24hours + "','',0,0,'','','','','','','','','','','','','','')";
                                            MySqlCommand cmd = new MySqlCommand(query, con);
                                            cmd.ExecuteNonQuery();
                                            cmd.Clone();
                                            if (con.State == System.Data.ConnectionState.Open)
                                            {
                                                con.Close();
                                                con.Dispose();
                                            }
                                        }

                                     //   _db.Companies.Add(_objCompany);
                                    }
                                }
                            }
                        }
                    }
                }

             //   _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
               .SelectMany(x => x.ValidationErrors)
               .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return View();
        }
    }
}