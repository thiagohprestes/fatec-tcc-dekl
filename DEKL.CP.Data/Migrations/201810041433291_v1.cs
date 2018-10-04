namespace DEKL.CP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Conta = c.String(nullable: false, maxLength: 10, unicode: false),
                        Saldo = c.Decimal(nullable: false, storeType: "money"),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Telefone = c.String(maxLength: 14, unicode: false),
                        Email = c.String(maxLength: 80, unicode: false),
                        TaxaChequeEspecial = c.Decimal(nullable: false, storeType: "money"),
                        TaxaEmprestimo = c.Decimal(nullable: false, storeType: "money"),
                        EncargResp = c.Double(nullable: false),
                        EnderecoId = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false, maxLength: 100, unicode: false),
                        CEP = c.String(maxLength: 8, unicode: false),
                        Numero = c.Int(),
                        Complemento = c.String(maxLength: 100, unicode: false),
                        Bairro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Cidade = c.String(nullable: false, maxLength: 50, unicode: false),
                        UF = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Credors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroSocial = c.String(),
                        Telefone = c.String(),
                        Contato = c.String(),
                        TelefoneContato = c.String(),
                        Prioridade = c.Short(nullable: false),
                        Email = c.String(),
                        EnderecoId = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(),
                        Telefone = c.String(maxLength: 14, unicode: false),
                        EnderecoId = c.Int(),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sobrenome = c.String(),
                        Email = c.String(nullable: false, maxLength: 80, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 88, fixedLength: true, unicode: false),
                        NivelAcesso = c.Short(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "UQ_dbo.Usuario.Email");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empresa", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Credors", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Banco", "EnderecoId", "dbo.Endereco");
            DropIndex("dbo.Usuario", "UQ_dbo.Usuario.Email");
            DropIndex("dbo.Empresa", new[] { "EnderecoId" });
            DropIndex("dbo.Credors", new[] { "EnderecoId" });
            DropIndex("dbo.Banco", new[] { "EnderecoId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Empresa");
            DropTable("dbo.Credors");
            DropTable("dbo.Endereco");
            DropTable("dbo.Banco");
            DropTable("dbo.Agencia");
        }
    }
}
