namespace Osobni_telefonski_imenik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Osoba", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Osoba", "UserID");
        }
    }
}
