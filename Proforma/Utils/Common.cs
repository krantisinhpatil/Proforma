using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Utils
{
    public static class Common
    {
        static SourcingGuideDevEntities _db = new SourcingGuideDevEntities();
        public static CompanyDetailsViewModel GetCompanyDetails(Company company)
        {
            CompanyDetailsViewModel _CompanyViewModel = new CompanyDetailsViewModel();
            _CompanyViewModel.CompanyId = company.CompanyId;
            _CompanyViewModel.PartnerType = "" + company.PartnerType;
            _CompanyViewModel.CompanyName = "" + company.CompanyName;
            _CompanyViewModel.StreetAddress = "" + company.StreetAddress;
            _CompanyViewModel.City = ""+company.City;
            _CompanyViewModel.State = "" + company.State;
            _CompanyViewModel.ZipCode = "" + company.ZipCode;
            _CompanyViewModel.Phone1 = "" + company.Phone1;
            _CompanyViewModel.Phone2 = "" + company.Phone2;
            _CompanyViewModel.Website = "" + company.Website;
            _CompanyViewModel.FTPSite = "" + company.FTPSite;
            _CompanyViewModel.ArtEmail = "" + company.ArtEmail;
            _CompanyViewModel.OrderEmail = "" + company.OrderEmail;
            _CompanyViewModel.FOBPointInCanada = "" + company.FOBPointInCanada;
            _CompanyViewModel.QuoteinCanadianDollars = "" + company.QuoteinCanadianDollars;
            _CompanyViewModel.PrimaryContactFirstName = "" + company.PrimaryContactFirstName;
            _CompanyViewModel.PrimaryContactLastName = "" + company.PrimaryContactLastName;
            _CompanyViewModel.PrimaryContactEmail = "" + company.PrimaryContactEmail;
            _CompanyViewModel.PrimaryContactFax = "" + company.PrimaryContactFax;
            _CompanyViewModel.PrimaryContactExtension = "" + company.PrimaryContactExtension;
            _CompanyViewModel.PrimaryContactTradeOnly = "" + company.PrimaryContactTradeOnly;
            _CompanyViewModel.PrimaryContactDirectLine = "" + company.PrimaryContactDirectLine;
            _CompanyViewModel.PrimaryContactAffiliations = "" + company.PrimaryContactAffiliations;
            _CompanyViewModel.SecondaryContactFirstName = "" + company.SecondaryContactFirstName;
            _CompanyViewModel.SecondaryContactLastName = "" + company.SecondaryContactLastName;
            _CompanyViewModel.SecondaryContactEmail = "" + company.SecondaryContactEmail;
            _CompanyViewModel.SecondaryContactExtension = "" + company.SecondaryContactExtension;
            _CompanyViewModel.SecondaryContactDirectLine = "" + company.SecondaryContactDirectLine;
            _CompanyViewModel.SecondaryContactFax = "" + company.SecondaryContactFax;
            _CompanyViewModel.SecondaryContactAffiliations = "" + company.SecondaryContactAffiliations;
            _CompanyViewModel.SecondaryContactTradeOnly = "" + company.SecondaryContactTradeOnly;
            _CompanyViewModel.TertiaryContactFirstName = "" + company.TertiaryContactFirstName;
            _CompanyViewModel.TertiaryContactLastName = "" + company.TertiaryContactLastName;
            _CompanyViewModel.TertiaryContactEmail = "" + company.TertiaryContactEmail;
            _CompanyViewModel.TertiaryContactFax = "" + company.TertiaryContactFax;
            _CompanyViewModel.TertiaryContactTradeOnly = "" + company.TertiaryContactTradeOnly;
            _CompanyViewModel.TertiaryContactDirectLine = "" + company.TertiaryContactDirectLine;
            _CompanyViewModel.TertiaryContactAffiliations = "" + company.TertiaryContactAffiliations;
            _CompanyViewModel.Union = company.Union;
            _CompanyViewModel.ASI = company.ASI;
            _CompanyViewModel.PPAI = company.PPAI;
            _CompanyViewModel.PPPC = company.PPPC;
            _CompanyViewModel.SAGE = company.SAGE;
            _CompanyViewModel.UPIC = company.UPIC;
            _CompanyViewModel.Certifications = company.Certifications;
            _CompanyViewModel.MinorityOwned = company.MinorityOwned;
            _CompanyViewModel.ProformaPricing = company.ProformaPricing;
            List<ProformaOffer> lstProformaPrograms = new List<ProformaOffer>();
            if (company.CompanyId > 0)
            {
                var _ProformaPrograms = _db.ProformaPrograms.Where(a => a.CompanyId == company.CompanyId).ToList();
                if (_ProformaPrograms.Count() > 0)
                {
                    foreach (var item in _ProformaPrograms)
                    {
                        if (!string.IsNullOrEmpty(item.Name))
                        {
                            lstProformaPrograms.Add(new ProformaOffer { Offer = item.Name });
                        }
                    }
                }
            }

            //if (!string.IsNullOrEmpty(company.ProformaPrograms))
            //{
            //    var lst = company.ProformaPrograms.Split(new[] { ',', '.' }).ToList();
            //    foreach (var pc in lst)
            //    {
            //        var strProformaOffer = pc.Trim();
            //        if (!string.IsNullOrEmpty(strProformaOffer))
            //        {
            //            lstProformaPrograms.Add(new ProformaOffer { Offer = strProformaOffer });
            //        }
            //    }
            //}
            _CompanyViewModel.ProformaOffers = lstProformaPrograms;

            List<ProductsNCapability> lstProducts = new List<ProductsNCapability>();
            if (company.CompanyId > 0)
            {
                var _ProductsNCapabilities = _db.ProductsCapabilities.Where(a => a.CompanyId == company.CompanyId).ToList();
                if (_ProductsNCapabilities.Count() > 0)
                {
                    foreach (var item in _ProductsNCapabilities)
                    {

                        if (!string.IsNullOrEmpty(item.Name))
                        {
                            lstProducts.Add(new ProductsNCapability { Product = item.Name });
                        }
                    }
                }
            }
            //if (!string.IsNullOrEmpty(company.ProductsNCapabilities))
            //{
            //    var lstProdcutsStrings = company.ProductsNCapabilities.Split(new[] { ',', '.', '\n', ';' }).ToList();
            //    foreach (var p in lstProdcutsStrings)
            //    {
            //        var strProduct = p.Trim();
            //        if (!string.IsNullOrEmpty(strProduct))
            //        {
            //            lstProducts.Add(new ProductsNCapability { Product = strProduct });
            //        }
            //    }
            //}
            _CompanyViewModel.ProductsNCapabilities = lstProducts;

            _CompanyViewModel.Rushor24hour = company.Rushor24hour;
            List<CompanyCategories> lstCompCat = new List<CompanyCategories>();
            var companycatategories = _db.CompanyCategories.Where(a => a.CompanyId == company.CompanyId).ToList();
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
            //_CompanyViewModel.CategoryId = company.CategoryId;
            _CompanyViewModel.Description = "" + company.Description;
            _CompanyViewModel.Latitude = null == company.Latitude ? 0 : company.Latitude;
            _CompanyViewModel.Longitude = null == company.Longitude ? 0 : company.Longitude;

            return _CompanyViewModel;
        }
    }
}