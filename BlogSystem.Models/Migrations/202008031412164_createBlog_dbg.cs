namespace BlogSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createBlog_dbg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodCount = c.Int(nullable: false),
                        BadCount = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImagePath = c.String(),
                        SiteName = c.String(),
                        FansCount = c.Int(nullable: false),
                        FocusCount = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleToCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        BlogCategoryId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId)
                .Index(t => t.ArticleId)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CategoryName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        Content = c.String(),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        FocusUserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        FocusUser1_Id = c.Guid(),
                        User1_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FocusUser1_Id)
                .ForeignKey("dbo.Users", t => t.User1_Id)
                .Index(t => t.FocusUser1_Id)
                .Index(t => t.User1_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fans", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.Fans", "FocusUser1_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleToCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "UserId", "dbo.Users");
            DropForeignKey("dbo.ArticleToCategories", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropIndex("dbo.Fans", new[] { "User1_Id" });
            DropIndex("dbo.Fans", new[] { "FocusUser1_Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.BlogCategories", new[] { "UserId" });
            DropIndex("dbo.ArticleToCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.ArticleToCategories", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropTable("dbo.Fans");
            DropTable("dbo.Comments");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.ArticleToCategories");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
