namespace EntityFramework123.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpgradeDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Gender");
        }
    }
}
