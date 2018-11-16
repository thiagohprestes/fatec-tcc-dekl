using DEKL.CP.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class AgenciaMap : EntityTypeConfiguration<Agencia>
    {
        public AgenciaMap()
        {
            //Table
            ToTable(nameof(Agencia));

            //Columns

            Property(e => e.Conta)
                .HasMaxLength(10)
                .IsRequired();

            Property(e => e.Saldo)
                .HasColumnType("money")
                .IsRequired();
        }   
    }
}
