using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class UserFavouritesModel
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CompanyDetailsViewModel> Companies { get; set; }
    }
}