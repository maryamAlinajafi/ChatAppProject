namespace ChatApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 80),
                        MemberCount = c.Int(nullable: false),
                        Semester = c.String(),
                        AccessCode = c.String(nullable: false, maxLength: 15),
                        AdminInfo = c.Int(nullable: false),
                        ProfileImage = c.String(),
                        UniversityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Universities", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.AccessCode, unique: true)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        SeenNumber = c.Int(),
                        DateTime = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.ClassId)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Firstname = c.String(maxLength: 15),
                        Lastname = c.String(maxLength: 15),
                        Username = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 15),
                        ProfileImage = c.String(),
                        Status = c.Boolean(),
                        EmailAddress = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Deadline = c.DateTime(),
                        Score = c.Int(),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        File = c.String(),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UniversityTitle = c.String(nullable: false),
                        UniversityCountry = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserClasses",
                c => new
                    {
                        User_ID = c.Guid(nullable: false),
                        Class_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Class_ID })
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_ID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.Class_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Resources", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Projects", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Messages", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserClasses", "Class_ID", "dbo.Classes");
            DropForeignKey("dbo.UserClasses", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Messages", "ClassId", "dbo.Classes");
            DropIndex("dbo.UserClasses", new[] { "Class_ID" });
            DropIndex("dbo.UserClasses", new[] { "User_ID" });
            DropIndex("dbo.Resources", new[] { "ClassId" });
            DropIndex("dbo.Projects", new[] { "ClassId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Messages", new[] { "User_ID" });
            DropIndex("dbo.Messages", new[] { "ClassId" });
            DropIndex("dbo.Classes", new[] { "UniversityId" });
            DropIndex("dbo.Classes", new[] { "AccessCode" });
            DropTable("dbo.UserClasses");
            DropTable("dbo.Universities");
            DropTable("dbo.Resources");
            DropTable("dbo.Projects");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Classes");
        }
    }
}
