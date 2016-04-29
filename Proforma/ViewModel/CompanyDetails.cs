using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    #region Company

    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class CompanySearchModel
    {
        public int CompanyId { get; set; }
        public List<CompanyCategories> CompanyCategories { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class CompanyDetailsViewModel
    {
        public int CompanyId { get; set; }
        public List<CompanyCategories> CompanyCategories { get; set; }
        public string PartnerType { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Website { get; set; }
        public string FTPSite { get; set; }
        public string ArtEmail { get; set; }
        public string OrderEmail { get; set; }
        public string FOBPointInCanada { get; set; }
        public string QuoteinCanadianDollars { get; set; }
        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string PrimaryContactExtension { get; set; }
        public string PrimaryContactDirectLine { get; set; }
        public string PrimaryContactFax { get; set; }
        public string PrimaryContactTradeOnly { get; set; }
        public string PrimaryContactAffiliations { get; set; }
        public string SecondaryContactFirstName { get; set; }
        public string SecondaryContactLastName { get; set; }
        public string SecondaryContactEmail { get; set; }
        public string SecondaryContactExtension { get; set; }
        public string SecondaryContactDirectLine { get; set; }
        public string SecondaryContactFax { get; set; }
        public string SecondaryContactAffiliations { get; set; }
        public string SecondaryContactTradeOnly { get; set; }
        public string TertiaryContactFirstName { get; set; }
        public string TertiaryContactLastName { get; set; }
        public string TertiaryContactEmail { get; set; }
        public string TertiaryContactDirectLine { get; set; }
        public string TertiaryContactFax { get; set; }
        public string TertiaryContactAffiliations { get; set; }
        public string TertiaryContactTradeOnly { get; set; }
        public string Union { get; set; }
        public string ASI { get; set; }
        public string PPAI { get; set; }
        public string PPPC { get; set; }
        public string SAGE { get; set; }
        public string UPIC { get; set; }
        public string Certifications { get; set; }
        public string MinorityOwned { get; set; }
        public string ProformaPricing { get; set; }
        public List<ProformaOffer> ProformaOffers { get; set; }
        public List<ProductsNCapability> ProductsNCapabilities { get; set; }
        public string Rushor24hour { get; set; }
        public bool IsFavourite { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Description { get; set; }
    }

    public class CompanyCoordinatesModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<ComapnyCoordinatesDetails> ComapnyCoordinates { get; set; }
    }

    public class ComapnyCoordinatesDetails
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Description { get; set; }
        public bool? IsFavourite { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public List<CompanyCategories> CompanyCategories { get; set; }
    }

    public class CompanyCategories
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProformaOffer
    {
        public string Offer { get; set; }
    }
    public class ProductsNCapability
    {
        public string Product { get; set; }
    }

    #endregion

    #region Category

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<FilterCriteriaViewModel> FilterCriterias { get; set; }
    }

    #endregion

    #region MVPLP

    public class MVPLPCompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int CategoryId { get; set; }
    }

    #endregion

    #region ProExclusives

    public class ProExclusiveViewModel
    {
        public int ProExclusiveId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ThumbnailPath { get; set; }
        public string PDFTitle { get; set; }
        public string PDFFilePath { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }

    }        
    
    #endregion

    }