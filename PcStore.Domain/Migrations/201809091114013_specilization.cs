namespace PcStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specilization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Specilization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Specilization");
        }
    }
}
