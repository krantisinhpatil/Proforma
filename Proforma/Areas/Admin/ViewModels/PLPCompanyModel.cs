using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class PLPCompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string CompanyStatus { get; set; }
    }
}