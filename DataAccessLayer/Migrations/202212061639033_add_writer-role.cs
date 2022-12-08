namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_writerrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "UserRole", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "UserRole");
        }
    }
}
