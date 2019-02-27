namespace DEKL.CP.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionRelashionshipAccountToPaytoPaymentSimulator : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountToPay", "PaymentSimulator_Id", "dbo.PaymentSimulator");
            DropForeignKey("dbo.AccountToPay", "PaymentSimulators_Id", "dbo.PaymentSimulator");
            DropIndex("dbo.AccountToPay", new[] { "PaymentSimulator_Id" });
            DropIndex("dbo.AccountToPay", new[] { "PaymentSimulators_Id" });
            CreateTable(
                "dbo.AccountToPayPaymentSimulator",
                c => new
                    {
                        AccountToPay_Id = c.Int(nullable: false),
                        PaymentSimulator_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountToPay_Id, t.PaymentSimulator_Id })
                .ForeignKey("dbo.AccountToPay", t => t.AccountToPay_Id)
                .ForeignKey("dbo.PaymentSimulator", t => t.PaymentSimulator_Id)
                .Index(t => t.AccountToPay_Id)
                .Index(t => t.PaymentSimulator_Id);
            
            DropColumn("dbo.AccountToPay", "PaymentSimulator_Id");
            DropColumn("dbo.AccountToPay", "PaymentSimulators_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountToPay", "PaymentSimulators_Id", c => c.Int());
            AddColumn("dbo.AccountToPay", "PaymentSimulator_Id", c => c.Int());
            DropForeignKey("dbo.AccountToPayPaymentSimulator", "PaymentSimulator_Id", "dbo.PaymentSimulator");
            DropForeignKey("dbo.AccountToPayPaymentSimulator", "AccountToPay_Id", "dbo.AccountToPay");
            DropIndex("dbo.AccountToPayPaymentSimulator", new[] { "PaymentSimulator_Id" });
            DropIndex("dbo.AccountToPayPaymentSimulator", new[] { "AccountToPay_Id" });
            DropTable("dbo.AccountToPayPaymentSimulator");
            CreateIndex("dbo.AccountToPay", "PaymentSimulators_Id");
            CreateIndex("dbo.AccountToPay", "PaymentSimulator_Id");
            AddForeignKey("dbo.AccountToPay", "PaymentSimulators_Id", "dbo.PaymentSimulator", "Id");
            AddForeignKey("dbo.AccountToPay", "PaymentSimulator_Id", "dbo.PaymentSimulator", "Id");
        }
    }
}
