using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Osobni_telefonski_imenik.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Osobni_telefonski_imenik.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Drzava.AddOrUpdate(
                new Drzava { Naziv = "Hrvatska"},
                new Drzava { Naziv = "Slovenija" },
                new Drzava { Naziv = "Finska" },
                new Drzava { Naziv = "�e�ka" },
                new Drzava { Naziv = "�panjolska" },
                new Drzava { Naziv = "�vedska"}
                );

            context.BrojTip.AddOrUpdate(
                new BrojTip {Naziv = "Ku�ni"},
                new BrojTip {Naziv = "Mobitel"},
                new BrojTip { Naziv = "Posao" }
                );
        }
    }
}
