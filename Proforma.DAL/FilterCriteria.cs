//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proforma.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FilterCriteria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FilterCriteria()
        {
            this.Pro_CompanyMeta = new HashSet<CompanyMeta>();
        }
    
        public int FilterCriteriaId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CriteriaText { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyMeta> Pro_CompanyMeta { get; set; }
    }
}
