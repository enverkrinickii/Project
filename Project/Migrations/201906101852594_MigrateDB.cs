namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        City = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        House = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentMessage = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ParentCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Weight = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Сatering_CateringId = c.Int(),
                    })
                .PrimaryKey(t => t.DishId)
                .ForeignKey("dbo.Сatering", t => t.Сatering_CateringId)
                .Index(t => t.Сatering_CateringId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ImageBytes = c.Binary(),
                        ThumbnailId = c.Int(),
                        Dish_DishId = c.Int(),
                        Sight_SightId = c.Int(),
                        Сatering_CateringId = c.Int(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Pictures", t => t.ThumbnailId)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId)
                .ForeignKey("dbo.Sights", t => t.Sight_SightId)
                .ForeignKey("dbo.Сatering", t => t.Сatering_CateringId)
                .Index(t => t.ThumbnailId)
                .Index(t => t.Dish_DishId)
                .Index(t => t.Sight_SightId)
                .Index(t => t.Сatering_CateringId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sights",
                c => new
                    {
                        SightId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Rating = c.Double(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SightId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Sight_SightId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Sights", t => t.Sight_SightId)
                .Index(t => t.Sight_SightId);
            
            CreateTable(
                "dbo.Сatering",
                c => new
                    {
                        CateringId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CateringId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.IngredientDishes",
                c => new
                    {
                        Ingredient_IngredientId = c.String(nullable: false, maxLength: 128),
                        Dish_DishId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_IngredientId, t.Dish_DishId })
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId, cascadeDelete: true)
                .Index(t => t.Ingredient_IngredientId)
                .Index(t => t.Dish_DishId);
            
            CreateTable(
                "dbo.СateringTag",
                c => new
                    {
                        Сatering_CateringId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Сatering_CateringId, t.Tag_TagId })
                .ForeignKey("dbo.Сatering", t => t.Сatering_CateringId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.Сatering_CateringId)
                .Index(t => t.Tag_TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Sight_SightId", "dbo.Sights");
            DropForeignKey("dbo.СateringTag", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.СateringTag", "Сatering_CateringId", "dbo.Сatering");
            DropForeignKey("dbo.Pictures", "Сatering_CateringId", "dbo.Сatering");
            DropForeignKey("dbo.Dishes", "Сatering_CateringId", "dbo.Сatering");
            DropForeignKey("dbo.Сatering", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Pictures", "Sight_SightId", "dbo.Sights");
            DropForeignKey("dbo.Sights", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pictures", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.Pictures", "ThumbnailId", "dbo.Pictures");
            DropForeignKey("dbo.IngredientDishes", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.IngredientDishes", "Ingredient_IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropIndex("dbo.СateringTag", new[] { "Tag_TagId" });
            DropIndex("dbo.СateringTag", new[] { "Сatering_CateringId" });
            DropIndex("dbo.IngredientDishes", new[] { "Dish_DishId" });
            DropIndex("dbo.IngredientDishes", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.Сatering", new[] { "AddressId" });
            DropIndex("dbo.Tags", new[] { "Sight_SightId" });
            DropIndex("dbo.Sights", new[] { "AddressId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Pictures", new[] { "Сatering_CateringId" });
            DropIndex("dbo.Pictures", new[] { "Sight_SightId" });
            DropIndex("dbo.Pictures", new[] { "Dish_DishId" });
            DropIndex("dbo.Pictures", new[] { "ThumbnailId" });
            DropIndex("dbo.Dishes", new[] { "Сatering_CateringId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.СateringTag");
            DropTable("dbo.IngredientDishes");
            DropTable("dbo.Сatering");
            DropTable("dbo.Tags");
            DropTable("dbo.Sights");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pictures");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Dishes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Addresses");
        }
    }
}
