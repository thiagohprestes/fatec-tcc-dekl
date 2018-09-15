using System.Data.Entity;

namespace DEKL.CP.Data.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<DEKLCPDataContextEF>
    {
        protected override void Seed(DEKLCPDataContextEF context)
        {
            context.SaveChanges();
        }
    }
}