using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proforma.ViewModel
{
    public class FilterCriteriaViewModel
    {
        public int FilterCriteriaId { get; set; }
        public int? CategoryId { get; set; }
        public string CriteriaText { get; set; }
    }
}