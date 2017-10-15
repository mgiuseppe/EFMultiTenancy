using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSharedTableSharedSchemasUsingDynamicFilters.TenantProvider
{
    public class TenantProvider : ITenantProvider
    {
        public Guid GetCurrentTenant() => new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1");
    }
}
