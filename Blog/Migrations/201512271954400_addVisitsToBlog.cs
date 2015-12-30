namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVisitsToBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Visits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "Visits");
        }
    }
}
