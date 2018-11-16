using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class PessoaFisicaMap : EntityTypeConfiguration<ProviderPhysicalPerson>
    {
        public PessoaFisicaMap()
        {
            //Table
            ToTable(nameof(ProviderPhysicalPerson));

            //Columns
        }
    }
}
