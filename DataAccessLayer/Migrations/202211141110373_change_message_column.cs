namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_message_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "MessageContent", c => c.String());
            DropColumn("dbo.Messages", "MessaegContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "MessaegContent", c => c.String());
            DropColumn("dbo.Messages", "MessageContent");
        }
    }
}
