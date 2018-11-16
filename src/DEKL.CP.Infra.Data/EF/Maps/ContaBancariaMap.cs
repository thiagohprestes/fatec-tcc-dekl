using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class ContaBancariaMap : EntityTypeConfiguration<ContaBancaria>
    {
        public ContaBancariaMap()
        {
            //Table
            ToTable(nameof(ContaBancaria));

            //Columns
        }
    }
}
