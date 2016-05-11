namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseGeneratedId2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users");
            DropForeignKey("dbo.Players", "UserId", "dbo.Users");
            DropForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "StockId", "dbo.Stocks");
            DropPrimaryKey("dbo.Admins");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.GameSettings");
            DropPrimaryKey("dbo.InvitationCodes");
            DropPrimaryKey("dbo.Players");
            DropPrimaryKey("dbo.Portfolios");
            DropPrimaryKey("dbo.StockHistories");
            DropPrimaryKey("dbo.Stocks");
            DropPrimaryKey("dbo.StockNews");
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Admins", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.GameSettings", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.InvitationCodes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Players", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Portfolios", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.StockHistories", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Stocks", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.StockNews", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Transactions", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Admins", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.GameSettings", "Id");
            AddPrimaryKey("dbo.InvitationCodes", "Id");
            AddPrimaryKey("dbo.Players", "Id");
            AddPrimaryKey("dbo.Portfolios", "Id");
            AddPrimaryKey("dbo.StockHistories", "Id");
            AddPrimaryKey("dbo.Stocks", "Id");
            AddPrimaryKey("dbo.StockNews", "Id");
            AddPrimaryKey("dbo.Transactions", "Id");
            AddForeignKey("dbo.Admins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Players", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios");
            DropForeignKey("dbo.Players", "UserId", "dbo.Users");
            DropForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users");
            DropForeignKey("dbo.Admins", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Transactions");
            DropPrimaryKey("dbo.StockNews");
            DropPrimaryKey("dbo.Stocks");
            DropPrimaryKey("dbo.StockHistories");
            DropPrimaryKey("dbo.Portfolios");
            DropPrimaryKey("dbo.Players");
            DropPrimaryKey("dbo.InvitationCodes");
            DropPrimaryKey("dbo.GameSettings");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Transactions", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.StockNews", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Stocks", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.StockHistories", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Portfolios", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Players", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.InvitationCodes", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.GameSettings", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Admins", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "Id");
            AddPrimaryKey("dbo.StockNews", "Id");
            AddPrimaryKey("dbo.Stocks", "Id");
            AddPrimaryKey("dbo.StockHistories", "Id");
            AddPrimaryKey("dbo.Portfolios", "Id");
            AddPrimaryKey("dbo.Players", "Id");
            AddPrimaryKey("dbo.InvitationCodes", "Id");
            AddPrimaryKey("dbo.GameSettings", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Admins", "Id");
            AddForeignKey("dbo.Transactions", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StockHistories", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Players", "PortfolioId", "dbo.Portfolios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Players", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
