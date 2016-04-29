using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class PLPCompanySortedModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public string Status { get; set; }
        public string PartnerType { get; set; }
    }
}