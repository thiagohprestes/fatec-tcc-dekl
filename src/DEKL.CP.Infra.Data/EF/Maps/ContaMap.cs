using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ContaMap : EntityTypeConfiguration<Conta>
    {
        public ContaMap()
        {
            //Table
            ToTable(nameof(Conta));

            //Columns
        }
    }
}
