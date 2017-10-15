using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSharedTableSharedSchemasUsingDynamicFilters.TenantProvider
{
    public static class TenantProviderFactory
    {
        public static ITenantProvider GetTenantProvider() => new TenantProvider();
    }
}
