using System;

namespace EFSharedTableSharedSchemasUsingDynamicFilters.Model
{
    class Prodotto : ISecuredByTenantId
    {
        public Guid? SecuredByTenantId { get; set; }

        public int ProdottoId { get; set; }
        public string Descrizione { get; set; }
    }
}
