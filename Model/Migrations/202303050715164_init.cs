namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BAIDANG", "IsPublic", c => c.Boolean(nullable: false));
            AddColumn("dbo.THELOAI", "Domain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.THELOAI", "Domain");
            DropColumn("dbo.BAIDANG", "IsPublic");
        }
    }
}
