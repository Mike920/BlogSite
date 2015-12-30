namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeaderUrlAndMiniatureUrlToBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "HeaderUrl", c => c.String(maxLength: 4000));
            AddColumn("dbo.Blogs", "MiniatureUrl", c => c.String(maxLength: 4000));
            DropColumn("dbo.Blogs", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "ImageUrl", c => c.String(maxLength: 4000));
            DropColumn("dbo.Blogs", "MiniatureUrl");
            DropColumn("dbo.Blogs", "HeaderUrl");
        }
    }
}
