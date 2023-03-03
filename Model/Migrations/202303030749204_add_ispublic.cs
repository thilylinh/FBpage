namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ispublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BAIDANG", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BAIDANG", "IsPublic");
        }
    }
}
