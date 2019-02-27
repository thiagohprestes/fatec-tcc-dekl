namespace DEKL.CP.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionPrecisionColumnNewBalanceBankTransaction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankTransaction", "NewBalance", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankTransaction", "NewBalance", c => c.Decimal(nullable: false, precision: 4, scale: 2));
        }
    }
}
