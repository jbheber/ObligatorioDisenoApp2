namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Portfolios", "PlayerId", "dbo.Players");
            DropIndex("dbo.Portfolios", new[] { "PlayerId" });
            AddColumn("dbo.Players", "PortfolioId", c => c.Guid(nullable: false));
            AddColumn("dbo.StockHistories", "StockId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Players", "PortfolioId");
            CreateIndex("dbo.StockHistories", "StockId");
            AddForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            DropColumn("dbo.Portfolios", "PlayerId");
            DropColumn("dbo.Portfolios", "ActionsValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Portfolios", "ActionsValue", c => c.Double(nullable: false));
            AddColumn("dbo.Portfolios", "PlayerId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios");
            DropIndex("dbo.StockHistories", new[] { "StockId" });
            DropIndex("dbo.Players", new[] { "PortfolioId" });
            DropColumn("dbo.StockHistories", "StockId");
            DropColumn("dbo.Players", "PortfolioId");
            CreateIndex("dbo.Portfolios", "PlayerId");
            AddForeignKey("dbo.Portfolios", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
