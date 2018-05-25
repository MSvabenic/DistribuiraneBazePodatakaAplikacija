namespace Osobni_telefonski_imenik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Osoba", "ImePrezime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Osoba", "ImePrezime");
        }
    }
}
