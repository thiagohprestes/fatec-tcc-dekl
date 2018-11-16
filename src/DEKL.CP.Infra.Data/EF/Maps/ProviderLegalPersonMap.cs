using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ProviderLegalPersonMap : EntityTypeConfiguration<ProviderLegalPerson>
    {
        public ProviderLegalPersonMap()
        {
            //Table
            ToTable(nameof(ProviderLegalPerson));

            //Columns

            Property(e => e.CorporateName)
                .IsRequired();
        }
    }
}
