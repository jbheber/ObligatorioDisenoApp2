namespace Stockapp.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitationCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvitationCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ParentUserId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ParentUserId, cascadeDelete: true)
                .Index(t => t.ParentUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvitationCodes", "ParentUserId", "dbo.Users");
            DropIndex("dbo.InvitationCodes", new[] { "ParentUserId" });
            DropTable("dbo.InvitationCodes");
        }
    }
}
