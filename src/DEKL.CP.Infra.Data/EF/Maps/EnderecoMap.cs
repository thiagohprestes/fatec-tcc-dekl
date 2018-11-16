using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class EnderecoMap : EntityTypeConfiguration<Address>
    {
        public EnderecoMap()
        {
            //Table
            ToTable(nameof(Address));

            //Columns
        }
    }
}
