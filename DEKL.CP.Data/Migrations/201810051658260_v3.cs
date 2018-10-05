namespace DEKL.CP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Administrador", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "Ativo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Usuario", "NivelAcesso");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "NivelAcesso", c => c.Short(nullable: false));
            DropColumn("dbo.Usuario", "Ativo");
            DropColumn("dbo.Usuario", "Administrador");
        }
    }
}
