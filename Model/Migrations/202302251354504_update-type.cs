namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.THELOAI", "Domain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.THELOAI", "Domain");
        }
    }
}
