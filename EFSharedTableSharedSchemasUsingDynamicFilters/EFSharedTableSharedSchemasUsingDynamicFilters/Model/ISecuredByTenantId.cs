using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSharedTableSharedSchemasUsingDynamicFilters.Model
{
    public interface ISecuredByTenantId
    {
        Guid? SecuredByTenantId { get; set; }
    }
}
