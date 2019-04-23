using System.Data.Entity.ModelConfiguration;
using DEKL.CP.Domain.Entities;

namespace DEKL.CP.Infra.Data.EF.Maps
{
    public class AuditMap : EntityTypeConfiguration<Audit>
    {
        public AuditMap()
        {
            //Table
            ToTable(nameof(Audit));

            //Columns
            Property(e => e.UpdatedRecordId);

            Property(e => e.Event)
                .IsMaxLength()
                .IsRequired();

            Property(e => e.DateTime);

            //Relationships
            HasRequired(e => e.ApplicationUser)
                .WithMany(e => e.Audits)
                .HasForeignKey(e => e.ApplicationUserId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.Module)
                .WithMany(e => e.Audits)
                .HasForeignKey(e => e.ModuleId)
                .WillCascadeOnDelete(false);
        }
    }
}
