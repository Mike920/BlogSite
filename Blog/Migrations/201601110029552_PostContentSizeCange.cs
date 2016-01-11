namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostContentSizeCange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false, maxLength: 4000));
        }
    }
}
