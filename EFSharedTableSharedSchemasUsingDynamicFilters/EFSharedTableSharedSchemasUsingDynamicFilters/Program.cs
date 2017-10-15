using EFSharedTableSharedSchemasUsingDynamicFilters.Model;
using EFSharedTableSharedSchemasUsingDynamicFilters.TenantProvider;
using System;
using System.Linq;

namespace EFSharedTableSharedSchemasUsingDynamicFilters
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MultiTenantContext())
            {
                var prodotto = new Prodotto() { Descrizione = "mela" + TenantProviderFactory.GetTenantProvider().GetCurrentTenant() };

                ctx.Prodotto.Add(prodotto);

                //for(int i = 0; i < 10000; i++)
                //    ctx.Prodotto.Add(new Prodotto { Descrizione = "provaMassiva" });

                ctx.SaveChanges();

                Console.Out.WriteLine("Prodotti:\n" + String.Join(",\n",ctx.Prodotto.Select(p => p.Descrizione)));
                Console.Out.WriteLine("Soggetti:\n" + String.Join(",\n", ctx.Soggetto.Select(s => s.Nome)));

                //...basta questo per vedere tutto il mondo
                Console.Out.WriteLine("Query:" + String.Join(",\n",ctx.Prodotto.SqlQuery("SELECT * FROM PRODOTTOES").Select(p => p.Descrizione)));
            }
        }
    }
}
