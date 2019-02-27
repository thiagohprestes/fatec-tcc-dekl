using DEKL.CP.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DEKL.CP.Infra.Data.EF.Context
{
    public class DEKLCPDataContextEF : DbContext
    {
        public DEKLCPDataContextEF() : base("DEKLCPConn")
        { }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAgency> BankAgencies { get; set; }
        public DbSet<AccountToPay> AccountToPays { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProviderPhysicalPerson> ProviderPhysicalPersons { get; set; }
        public DbSet<ProviderLegalPerson> ProviderLegalPersons { get; set; }
        public DbSet<ProviderBankAccount> ProviderBankAccounts { get; set; }
        public DbSet<InternalBankAccount> InternalBankAccounts { get; set; }
        public DbSet<PaymentSimulator> PaymentSimulators { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == nameof(EntityBase.Id))
                .Configure(p =>
                {
                    p.IsKey();
                    p.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                });

            modelBuilder.Properties<string>()
                .Configure(e => e.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(e => e.HasMaxLength(100));

            modelBuilder.Properties<decimal>()
                .Configure(e => e.HasPrecision(4, 2));

            modelBuilder.Configurations.Add(new Maps.ModuleMap());
            modelBuilder.Configurations.Add(new Maps.AuditMap());
            modelBuilder.Configurations.Add(new Maps.StateMap());
            modelBuilder.Configurations.Add(new Maps.AddressMap());
            modelBuilder.Configurations.Add(new Maps.BankMap());
            modelBuilder.Configurations.Add(new Maps.BankAgencyMap());
            modelBuilder.Configurations.Add(new Maps.AccountToPayMap());
            modelBuilder.Configurations.Add(new Maps.InstallmentMap());
            modelBuilder.Configurations.Add(new Maps.ProviderMap());
            modelBuilder.Configurations.Add(new Maps.ProviderPhysicalPersonMap());
            modelBuilder.Configurations.Add(new Maps.ProviderLegalPersonMap());
            modelBuilder.Configurations.Add(new Maps.ProviderBankAccountMap());
            modelBuilder.Configurations.Add(new Maps.InternalBankAccountMap());
            modelBuilder.Configurations.Add(new Maps.BankTransactionMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}