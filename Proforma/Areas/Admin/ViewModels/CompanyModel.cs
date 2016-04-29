using Proforma.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.Areas.Admin.ViewModels
{
    public class CompanyModel
    {
        public ContactInfoModel ContactInfo { get; set; }
        public ProductsCapabilityModel ProductsCapability { get; set; }
        public ProformaProgramModel ProformaProgram { get; set; }
    }

    public class ProductsCapabilityModel
    {
        public int CompanyId { get; set; }
        public int ProductsCapabilityId { get; set; }
        public string Name { get; set; }
        public List<ProductsCapability> lstProductsCapabilities { get; set; }
    }

    public class ProformaProgramModel
    {
        public int CompanyId { get; set; }
        public int ProformaProgramId { get; set; }
        public string Name { get; set; }
        public List<ProformaProgram> lstProformaProgram { get; set; }
    }
}