using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class PLPCompanyInfoModel
    {
        public int CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string ASI { get; set; }
        public string PPAI { get; set; }
        public string SAGE { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Website { get; set; }
        public string FOBPointInCanada { get; set; }

        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactDirectLine { get; set; }
        public string PrimaryContactFax { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string PrimaryContactAffilations { get; set; }
        public string PrimaryContactTradeOnly { get; set; }

        public string SecondaryContactFirstName { get; set; }
        public string SecondaryContactLastName { get; set; }
        public string SecondaryContactDirectLine { get; set; }
        public string SecondaryContactFax { get; set; }
        public string SecondaryContactEmail { get; set; }
        public string SecondaryContactAffilations { get; set; }
        public string SecondaryContactTradeOnly { get; set; }

        public string TertiaryContactFirstName { get; set; }
        public string TertiaryContactLastName { get; set; }
        public string TertiaryContactDirectLine { get; set; }
        public string TertiaryContactFax { get; set; }
        public string TertiaryContactEmail { get; set; }
        public string TertiaryContactAffilations { get; set; }
        public string TertiaryContactTradeOnly { get; set; }
        public string CompanyLogo { get; set; }
        public string Description { get; set; }
        public List<int> lstAllCategories { get; set; }
    }
}