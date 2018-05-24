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
                x => x.DrzavaID,
                new Drzava { Naziv = "Hrvatska" },
                new Drzava { Naziv = "Slovenija" },
                new Drzava { Naziv = "Èeška" }
            );

            context.BrojTip.AddOrUpdate(
               x => x.Naziv,
               new BrojTip { Naziv = "Kuæni"},
               new BrojTip { Naziv = "Posao" },
               new BrojTip { Naziv = "Mobitel" }
               );
        }
    }
}
