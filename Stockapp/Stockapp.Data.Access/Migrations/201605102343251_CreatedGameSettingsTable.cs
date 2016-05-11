namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedGameSettingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InitialMoney = c.Double(nullable: false),
                        MaxTransactionsPerDay = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameSettings");
        }
    }
}
