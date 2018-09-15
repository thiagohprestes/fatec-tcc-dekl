using DEKL.CP.Domain.Entities;
using System.Data.Entity;

namespace DEKL.CP.Data.EF
{
    public class DEKLCPDataContextEF : DbContext
    {
        public DEKLCPDataContextEF() : base("DEKLCPConn") 
            => Database.SetInitializer(new DbInitializer());

        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<ContaBancaria> ContasBancarias { get; set; }
        public DbSet<Credor> Credores { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<SimulaConta> SimulaContas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Maps.EnderecoMap());
        }
    }
}
