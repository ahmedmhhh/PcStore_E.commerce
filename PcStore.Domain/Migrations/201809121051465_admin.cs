namespace PcStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: true));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: true));
            AlterColumn("dbo.Products", "Specilization", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Specilization", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
