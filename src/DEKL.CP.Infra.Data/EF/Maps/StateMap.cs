using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            //Table
            ToTable(nameof(State));

            //Columns
            Property(e => e.Name)
                .HasMaxLength(60)
                .IsRequired();

            Property(e => e.Initials)
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}
