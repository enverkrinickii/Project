namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initmigration : DbMigration
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
                "dbo.ChatRooms",
                c => new
                    {
                        ChatRoomId = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChatRoomId)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ImageBytes = c.Binary(),
                        ThumbnailId = c.Int(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Pictures", t => t.ThumbnailId)
                .Index(t => t.ThumbnailId);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        DishId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Weight = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Catering_CateringId = c.Int(),
                    })
                .PrimaryKey(t => t.DishId)
                .ForeignKey("dbo.Caterings", t => t.Catering_CateringId)
                .Index(t => t.Catering_CateringId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId);
            
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
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Caterings",
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
                "dbo.Localizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        Culture = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        UserId = c.String(maxLength: 128),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
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
                "dbo.DishPictures",
                c => new
                    {
                        Dish_DishId = c.Int(nullable: false),
                        Picture_PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dish_DishId, t.Picture_PictureId })
                .ForeignKey("dbo.Dishes", t => t.Dish_DishId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_PictureId, cascadeDelete: true)
                .Index(t => t.Dish_DishId)
                .Index(t => t.Picture_PictureId);
            
            CreateTable(
                "dbo.SightPictures",
                c => new
                    {
                        Sight_SightId = c.Int(nullable: false),
                        Picture_PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sight_SightId, t.Picture_PictureId })
                .ForeignKey("dbo.Sights", t => t.Sight_SightId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_PictureId, cascadeDelete: true)
                .Index(t => t.Sight_SightId)
                .Index(t => t.Picture_PictureId);
            
            CreateTable(
                "dbo.SightTags",
                c => new
                    {
                        Sight_SightId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new {  t.Sight_SightId, t.Tag_TagId, })
                .ForeignKey("dbo.Sights", t => t.Sight_SightId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Sight_SightId);
            
            CreateTable(
                "dbo.CateringPictures",
                c => new
                    {
                        Catering_CateringId = c.Int(nullable: false),
                        Picture_PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Catering_CateringId, t.Picture_PictureId })
                .ForeignKey("dbo.Caterings", t => t.Catering_CateringId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_PictureId, cascadeDelete: true)
                .Index(t => t.Catering_CateringId)
                .Index(t => t.Picture_PictureId);
            
            CreateTable(
                "dbo.CateringTags",
                c => new
                    {
                        Catering_CateringId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Catering_CateringId, t.Tag_TagId })
                .ForeignKey("dbo.Caterings", t => t.Catering_CateringId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.Catering_CateringId)
                .Index(t => t.Tag_TagId);
            
            CreateTable(
                "dbo.FavoriteCaterings",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Catering_CateringId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Catering_CateringId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Caterings", t => t.Catering_CateringId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Catering_CateringId);
            
            CreateTable(
                "dbo.UserChatRooms",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ChatRoom_ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ChatRoom_ChatRoomId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoom_ChatRoomId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ChatRoom_ChatRoomId);
            
            CreateTable(
                "dbo.FavoriteSights",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Sight_SightId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Sight_SightId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sights", t => t.Sight_SightId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Sight_SightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.ChatRooms", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "ThumbnailId", "dbo.Pictures");
            DropForeignKey("dbo.ApplicationUserSights", "Sight_SightId", "dbo.Sights");
            DropForeignKey("dbo.ApplicationUserSights", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserChatRooms", "ChatRoom_ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.ApplicationUserChatRooms", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserCaterings", "Catering_CateringId", "dbo.Caterings");
            DropForeignKey("dbo.ApplicationUserCaterings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CateringTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.CateringTags", "Catering_CateringId", "dbo.Caterings");
            DropForeignKey("dbo.CateringPictures", "Picture_PictureId", "dbo.Pictures");
            DropForeignKey("dbo.CateringPictures", "Catering_CateringId", "dbo.Caterings");
            DropForeignKey("dbo.Dishes", "Catering_CateringId", "dbo.Caterings");
            DropForeignKey("dbo.Caterings", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.TagSights", "Sight_SightId", "dbo.Sights");
            DropForeignKey("dbo.TagSights", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.SightPictures", "Picture_PictureId", "dbo.Pictures");
            DropForeignKey("dbo.SightPictures", "Sight_SightId", "dbo.Sights");
            DropForeignKey("dbo.Sights", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.DishPictures", "Picture_PictureId", "dbo.Pictures");
            DropForeignKey("dbo.DishPictures", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.IngredientDishes", "Dish_DishId", "dbo.Dishes");
            DropForeignKey("dbo.IngredientDishes", "Ingredient_IngredientId", "dbo.Ingredients");
            DropIndex("dbo.ApplicationUserSights", new[] { "Sight_SightId" });
            DropIndex("dbo.ApplicationUserSights", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserChatRooms", new[] { "ChatRoom_ChatRoomId" });
            DropIndex("dbo.ApplicationUserChatRooms", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserCaterings", new[] { "Catering_CateringId" });
            DropIndex("dbo.ApplicationUserCaterings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CateringTags", new[] { "Tag_TagId" });
            DropIndex("dbo.CateringTags", new[] { "Catering_CateringId" });
            DropIndex("dbo.CateringPictures", new[] { "Picture_PictureId" });
            DropIndex("dbo.CateringPictures", new[] { "Catering_CateringId" });
            DropIndex("dbo.TagSights", new[] { "Sight_SightId" });
            DropIndex("dbo.TagSights", new[] { "Tag_TagId" });
            DropIndex("dbo.SightPictures", new[] { "Picture_PictureId" });
            DropIndex("dbo.SightPictures", new[] { "Sight_SightId" });
            DropIndex("dbo.DishPictures", new[] { "Picture_PictureId" });
            DropIndex("dbo.DishPictures", new[] { "Dish_DishId" });
            DropIndex("dbo.IngredientDishes", new[] { "Dish_DishId" });
            DropIndex("dbo.IngredientDishes", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "ChatRoomId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Caterings", new[] { "AddressId" });
            DropIndex("dbo.Sights", new[] { "AddressId" });
            DropIndex("dbo.Dishes", new[] { "Catering_CateringId" });
            DropIndex("dbo.Pictures", new[] { "ThumbnailId" });
            DropIndex("dbo.ChatRooms", new[] { "PictureId" });
            DropTable("dbo.ApplicationUserSights");
            DropTable("dbo.ApplicationUserChatRooms");
            DropTable("dbo.ApplicationUserCaterings");
            DropTable("dbo.CateringTags");
            DropTable("dbo.CateringPictures");
            DropTable("dbo.TagSights");
            DropTable("dbo.SightPictures");
            DropTable("dbo.DishPictures");
            DropTable("dbo.IngredientDishes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.Localizations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Caterings");
            DropTable("dbo.Tags");
            DropTable("dbo.Sights");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Dishes");
            DropTable("dbo.Pictures");
            DropTable("dbo.ChatRooms");
            DropTable("dbo.Addresses");
        }
    }
}
