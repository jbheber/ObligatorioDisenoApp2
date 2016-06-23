namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PortfolioId = c.Long(nullable: false),
                        StockId = c.Long(nullable: false),
                        QuantityOfActions = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.PortfolioId)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AvailableMoney = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StockId = c.Long(nullable: false),
                        StockQuantity = c.Int(nullable: false),
                        TotalValue = c.Double(nullable: false),
                        TransactionDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Type = c.Int(nullable: false),
                        PortfolioId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.PortfolioId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        QuantiyOfActions = c.Double(nullable: false),
                        UnityValue = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockHistories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateOfChange = c.DateTimeOffset(nullable: false, precision: 7),
                        RecordedValue = c.Double(nullable: false),
                        StockId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.StockNews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PublicationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Title = c.String(),
                        Content = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Stock_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.Stock_Id)
                .Index(t => t.Stock_Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CI = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Email = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InitialMoney = c.Double(nullable: false),
                        MaxTransactionsPerDay = c.Int(nullable: false),
                        RecomendationAlgorithm = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvitationCodes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        ParentUserId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ParentUserId, cascadeDelete: true)
                .Index(t => t.ParentUserId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CI = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        Name = c.String(maxLength: 30),
                        Surname = c.String(maxLength: 30),
                        Email = c.String(),
                        PortfolioId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PortfolioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "UserId", "dbo.Users");
            DropForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Actions", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockNews", "Stock_Id", "dbo.Stocks");
            DropForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Actions", "PortfolioId", "dbo.Portfolios");
            DropIndex("dbo.Players", new[] { "PortfolioId" });
            DropIndex("dbo.Players", new[] { "UserId" });
            DropIndex("dbo.InvitationCodes", new[] { "ParentUserId" });
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropIndex("dbo.StockNews", new[] { "Stock_Id" });
            DropIndex("dbo.StockHistories", new[] { "StockId" });
            DropIndex("dbo.Transactions", new[] { "PortfolioId" });
            DropIndex("dbo.Transactions", new[] { "StockId" });
            DropIndex("dbo.Actions", new[] { "StockId" });
            DropIndex("dbo.Actions", new[] { "PortfolioId" });
            DropTable("dbo.Players");
            DropTable("dbo.InvitationCodes");
            DropTable("dbo.GameSettings");
            DropTable("dbo.Users");
            DropTable("dbo.Admins");
            DropTable("dbo.StockNews");
            DropTable("dbo.StockHistories");
            DropTable("dbo.Stocks");
            DropTable("dbo.Transactions");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Actions");
        }
    }
}
