namespace Osobni_telefonski_imenik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KreiranjeTablica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brojevi",
                c => new
                    {
                        BrojID = c.Guid(nullable: false, identity: true),
                        OsobaID = c.Guid(nullable: false),
                        BrojTipID = c.Guid(nullable: false),
                        Broj = c.String(),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.BrojID)
                .ForeignKey("dbo.BrojTip", t => t.BrojTipID, cascadeDelete: true)
                .ForeignKey("dbo.Osoba", t => t.OsobaID, cascadeDelete: true)
                .Index(t => t.OsobaID)
                .Index(t => t.BrojTipID);
            
            CreateTable(
                "dbo.BrojTip",
                c => new
                    {
                        BrojTipID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.BrojTipID);
            
            CreateTable(
                "dbo.Osoba",
                c => new
                    {
                        OsobaID = c.Guid(nullable: false, identity: true),
                        GradID = c.Guid(nullable: false),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.OsobaID)
                .ForeignKey("dbo.Grad", t => t.GradID, cascadeDelete: true)
                .Index(t => t.GradID);
            
            CreateTable(
                "dbo.Grad",
                c => new
                    {
                        GradID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                        DrzavaID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.GradID)
                .ForeignKey("dbo.Drzava", t => t.DrzavaID, cascadeDelete: true)
                .Index(t => t.DrzavaID);
            
            CreateTable(
                "dbo.Drzava",
                c => new
                    {
                        DrzavaID = c.Guid(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.DrzavaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brojevi", "OsobaID", "dbo.Osoba");
            DropForeignKey("dbo.Osoba", "GradID", "dbo.Grad");
            DropForeignKey("dbo.Grad", "DrzavaID", "dbo.Drzava");
            DropForeignKey("dbo.Brojevi", "BrojTipID", "dbo.BrojTip");
            DropIndex("dbo.Grad", new[] { "DrzavaID" });
            DropIndex("dbo.Osoba", new[] { "GradID" });
            DropIndex("dbo.Brojevi", new[] { "BrojTipID" });
            DropIndex("dbo.Brojevi", new[] { "OsobaID" });
            DropTable("dbo.Drzava");
            DropTable("dbo.Grad");
            DropTable("dbo.Osoba");
            DropTable("dbo.BrojTip");
            DropTable("dbo.Brojevi");
        }
    }
}
