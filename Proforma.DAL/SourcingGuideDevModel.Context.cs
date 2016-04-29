﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SourcingGuideDevEntities : DbContext
    {
        public SourcingGuideDevEntities()
            : base("name=SourcingGuideDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SourcingGuideDevUser> SourcingGuideDevUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyMeta> CompanyMetas { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FilterCriteria> FilterCriterias { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<ProductsCapability> ProductsCapabilities { get; set; }
        public virtual DbSet<ProformaGeneralSettings> ProformaGeneralSettings { get; set; }
        public virtual DbSet<ProformaProgram> ProformaPrograms { get; set; }
        public virtual DbSet<ProformaUsers> ProformaUsers { get; set; }
        public virtual DbSet<SPDevTeam> SPDevTeams { get; set; }
        public virtual DbSet<SPDevTeamMember> SPDevTeamMembers { get; set; }
        public virtual DbSet<SupportCenterContact> SupportCenterContacts { get; set; }
        public virtual DbSet<TermsCondition> TermsConditions { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<CompanyCategory> CompanyCategories { get; set; }
        public virtual DbSet<ProExclusive> ProExclusives { get; set; }
    
        public virtual ObjectResult<Pro_GetCompanies_Result> Pro_GetCompanies(Nullable<int> categoryID, string companyName, string partnerType, string status, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var categoryIDParameter = categoryID.HasValue ?
                new ObjectParameter("CategoryID", categoryID) :
                new ObjectParameter("CategoryID", typeof(int));
    
            var companyNameParameter = companyName != null ?
                new ObjectParameter("CompanyName", companyName) :
                new ObjectParameter("CompanyName", typeof(string));
    
            var partnerTypeParameter = partnerType != null ?
                new ObjectParameter("PartnerType", partnerType) :
                new ObjectParameter("PartnerType", typeof(string));
    
            var statusParameter = status != null ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_GetCompanies_Result>("Pro_GetCompanies", categoryIDParameter, companyNameParameter, partnerTypeParameter, statusParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    }
}
