namespace DEKL.CP.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankAgency", "UQ_dbo.BankAgency.Number");
            AddColumn("dbo.Provider", "TypeProvider", c => c.Int(nullable: false));
            AlterColumn("dbo.BankAgency", "Number", c => c.Short(nullable: false));
            CreateIndex("dbo.BankAgency", "Number", unique: true, name: "UQ_dbo.BankAgency.Number");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BankAgency", "UQ_dbo.BankAgency.Number");
            AlterColumn("dbo.BankAgency", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.Provider", "TypeProvider");
            CreateIndex("dbo.BankAgency", "Number", unique: true, name: "UQ_dbo.BankAgency.Number");
        }
    }
}
