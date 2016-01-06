namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrlSlugs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogCategories", "UrlSlug", c => c.String(maxLength: 4000));
            AddColumn("dbo.PostCategories", "UrlSlug", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "UrlSlug");
            DropColumn("dbo.BlogCategories", "UrlSlug");
        }
    }
}
