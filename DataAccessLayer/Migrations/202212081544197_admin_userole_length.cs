namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin_userole_length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "AdminUserRole", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdminUserRole", c => c.String(maxLength: 1));
        }
    }
}
