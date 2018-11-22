namespace DEKL.CP.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientKey = c.String(maxLength: 100, unicode: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.ApplicationUser_Id);
            
            //CreateTable(
            //    "dbo.ApplicationUser",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
            //            LastName = c.String(maxLength: 50, unicode: false),
            //            AddedDate = c.DateTime(nullable: false),
            //            ModifiedDate = c.DateTime(),
            //            Active = c.Boolean(nullable: false),
            //            Email = c.String(maxLength: 100, unicode: false),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(maxLength: 100, unicode: false),
            //            SecurityStamp = c.String(maxLength: 100, unicode: false),
            //            PhoneNumber = c.String(maxLength: 100, unicode: false),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(maxLength: 100, unicode: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(maxLength: 100, unicode: false),
                        ClaimValue = c.String(maxLength: 100, unicode: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 100, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 100, unicode: false),
                        UserId = c.Int(nullable: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Clients", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserRole", "RoleId", "dbo.ApplicationRole");
            DropIndex("dbo.ApplicationUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserRole", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRole", "RoleNameIndex");
            DropIndex("dbo.Clients", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserLogin");
            DropTable("dbo.ApplicationUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.ApplicationUserRole");
            DropTable("dbo.ApplicationRole");
            DropTable("dbo.Clients");
            DropTable("dbo.Claims");
        }
    }
}
