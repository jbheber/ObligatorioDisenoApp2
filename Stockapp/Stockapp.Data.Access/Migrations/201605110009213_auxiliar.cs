namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auxiliar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameSettings", "RecomendationAlgorithm", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSettings", "RecomendationAlgorithm");
        }
    }
}
