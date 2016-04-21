namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Stock = c.String(),
                        UnityValue = c.Double(nullable: false),
                        Var = c.Double(nullable: false),
                        PercentageVar = c.Double(nullable: false),
                        MarketCapital = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        PortfolioId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .Index(t => t.PortfolioId);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        AvailableMoney = c.Double(nullable: false),
                        ActionsValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CI = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InvitationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CI = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Actions", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolios", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "UserId", "dbo.Users");
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.Portfolios", new[] { "PlayerId" });
            DropIndex("dbo.Actions", new[] { "PortfolioId" });
            DropTable("dbo.Admins");
            DropTable("dbo.Users");
            DropTable("dbo.Players");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Actions");
        }
    }
}
