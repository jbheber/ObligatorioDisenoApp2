namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actions", "PortfolioId", "dbo.Portfolios");
            DropIndex("dbo.Actions", new[] { "PortfolioId" });
            CreateTable(
                "dbo.StockHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfChange = c.DateTimeOffset(nullable: false, precision: 7),
                        RecordedValue = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockNews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublicationDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Title = c.String(),
                        Content = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        UnityValue = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StockId = c.Guid(nullable: false),
                        NetVariation = c.Double(nullable: false),
                        PercentageVariation = c.Double(nullable: false),
                        MarketCapital = c.Double(nullable: false),
                        StockQuantity = c.Int(nullable: false),
                        TotalValue = c.Double(nullable: false),
                        TransactionDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Type = c.Int(nullable: false),
                        PortfolioId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.PortfolioId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.PortfolioId);
            
            AddColumn("dbo.Portfolios", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Players", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Admins", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.Users", "InvitationCode");
            DropTable("dbo.Actions");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "InvitationCode", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Transactions", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios");
            DropIndex("dbo.Transactions", new[] { "PortfolioId" });
            DropIndex("dbo.Transactions", new[] { "StockId" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Admins", "IsDeleted");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Players", "IsDeleted");
            DropColumn("dbo.Portfolios", "IsDeleted");
            DropTable("dbo.Transactions");
            DropTable("dbo.Stocks");
            DropTable("dbo.StockNews");
            DropTable("dbo.StockHistories");
            CreateIndex("dbo.Actions", "PortfolioId");
            AddForeignKey("dbo.Actions", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
        }
    }
}
