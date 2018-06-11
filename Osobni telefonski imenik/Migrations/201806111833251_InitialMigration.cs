namespace Osobni_telefonski_imenik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrojTip",
                c => new
                    {
                        BrojTipID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.BrojTipID);
            
            CreateTable(
                "dbo.Drzava",
                c => new
                    {
                        DrzavaID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DrzavaID);
            
            CreateTable(
                "dbo.Grad",
                c => new
                    {
                        GradID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(nullable: false),
                        DrzavaID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.GradID)
                .ForeignKey("dbo.Drzava", t => t.DrzavaID, cascadeDelete: true)
                .Index(t => t.DrzavaID);
            
            CreateTable(
                "dbo.Osoba",
                c => new
                    {
                        OsobaID = c.Guid(nullable: false, identity: true),
                        UserID = c.String(),
                        ImePrezime = c.String(),
                        GradID = c.Guid(nullable: false),
                        Ime = c.String(nullable: false),
                        Prezime = c.String(nullable: false),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.OsobaID)
                .ForeignKey("dbo.Grad", t => t.GradID, cascadeDelete: true)
                .Index(t => t.GradID);
            
            CreateTable(
                "dbo.OsobaBroj",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        OsobaID = c.Guid(nullable: false),
                        BrojID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brojevi", t => t.BrojID, cascadeDelete: true)
                .ForeignKey("dbo.Osoba", t => t.OsobaID, cascadeDelete: true)
                .Index(t => t.OsobaID)
                .Index(t => t.BrojID);
            
            CreateTable(
                "dbo.Brojevi",
                c => new
                    {
                        ID = c.Guid(nullable: false, identity: true),
                        BrojTipID = c.Guid(nullable: false),
                        Broj = c.String(),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BrojTip", t => t.BrojTipID, cascadeDelete: true)
                .Index(t => t.BrojTipID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OsobaBroj", "OsobaID", "dbo.Osoba");
            DropForeignKey("dbo.OsobaBroj", "BrojID", "dbo.Brojevi");
            DropForeignKey("dbo.Brojevi", "BrojTipID", "dbo.BrojTip");
            DropForeignKey("dbo.Osoba", "GradID", "dbo.Grad");
            DropForeignKey("dbo.Grad", "DrzavaID", "dbo.Drzava");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Brojevi", new[] { "BrojTipID" });
            DropIndex("dbo.OsobaBroj", new[] { "BrojID" });
            DropIndex("dbo.OsobaBroj", new[] { "OsobaID" });
            DropIndex("dbo.Osoba", new[] { "GradID" });
            DropIndex("dbo.Grad", new[] { "DrzavaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Brojevi");
            DropTable("dbo.OsobaBroj");
            DropTable("dbo.Osoba");
            DropTable("dbo.Grad");
            DropTable("dbo.Drzava");
            DropTable("dbo.BrojTip");
        }
    }
}
