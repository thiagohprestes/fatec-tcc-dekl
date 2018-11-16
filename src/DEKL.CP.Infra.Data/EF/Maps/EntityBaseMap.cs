using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Contracts.Repositories;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class EntityBaseMap : EntityTypeConfiguration<IEntityBase>
    {
        public EntityBaseMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.AddedDate);

            Property(e => e.ModifiedDate);

            Property(e => e.Active);
        }
    }
}
