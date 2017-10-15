using System;
using System.Data.Entity;
using EntityFramework.DynamicFilters;
using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using EFSharedTableSharedSchemasUsingDynamicFilters.TenantProvider;

namespace EFSharedTableSharedSchemasUsingDynamicFilters.Model
{
    class MultiTenantContext : DbContext
    {
        public DbSet<Prodotto> Prodotto { get; set; }
        public DbSet<Soggetto> Soggetto { get; set; }

        private Guid CurrentTenantId { get; }

        public MultiTenantContext() : base("name=MyTestConnectionString")
        {
            CurrentTenantId = TenantProviderFactory.GetTenantProvider().GetCurrentTenant();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MultiTenantContext, Migrations.Configuration>());

            //Initialize DynamicFilters and set the value of the parameter currentTenantId for the SecuredByTenant filter
            this.InitializeDynamicFilters();
            this.SetFilterScopedParameterValue("SecuredByTenant", "currentTenantId", CurrentTenantId);
            this.SetFilterGlobalParameterValue("SecuredByTenant", "currentTenantId", CurrentTenantId);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //create a "SecuredByTenant" filter for every DbSet that implements ISecuredByTenantId
            modelBuilder.Filter("SecuredByTenant"
                                , (ISecuredByTenantId securedByTenant, Guid currentTenantId) => securedByTenant.SecuredByTenantId == currentTenantId
                                , () => Guid.Empty);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //set tenant id for each ISecuredByTenantId entry created
            foreach(var entry in GetCreatedEntries().Where(e => e.Entity is ISecuredByTenantId).Select(e => e.Entity as ISecuredByTenantId))
                entry.SecuredByTenantId = CurrentTenantId;

            return base.SaveChanges();
        }

        private IEnumerable<DbEntityEntry> GetCreatedEntries() {
            return ChangeTracker.Entries().Where(V => V.State.HasFlag(EntityState.Added));
        }
    }
}
