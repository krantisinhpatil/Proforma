using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class CompaniesModel
    {
        public int CompanyId { get; set; }
        public string PartnerType { get; set; }
        public string CompanyName { get; set; }
        public string ASI { get; set; }
        public string PPAI { get; set; }
        public string PPPC { get; set; }
        public string SAGE { get; set; }
        public string UPIC { get; set; }
    }
}