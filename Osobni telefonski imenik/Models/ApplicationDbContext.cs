using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Osobni_telefonski_imenik.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Drzava> Drzava { get; set; }

        public DbSet<Grad> Grad{ get; set; }

        public DbSet<BrojTip> BrojTip { get; set; }

        public DbSet<Osoba> Osoba { get; set; }

        public DbSet<OsobaBroj> OsobaBroj { get; set; }


        public ApplicationDbContext()
            : base("TelefonskiImenikLocalDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Drzava>()
                .HasKey(x => x.DrzavaID);

            modelBuilder.Entity<Grad>()
                .HasKey(x => x.GradID);
            modelBuilder.Entity<Grad>()
                .HasRequired(x => x.Drzava);

            modelBuilder.Entity<BrojTip>()
                .HasKey(x => x.BrojTipID);

            modelBuilder.Entity<Osoba>()
                .HasKey(x => x.OsobaID);
            modelBuilder.Entity<Osoba>()
                .HasRequired(x => x.Grad);

            modelBuilder.Entity<OsobaBroj>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<OsobaBroj>()
                .HasRequired(x => x.Osoba);
            modelBuilder.Entity<OsobaBroj>()
                .HasRequired(x => x.BrojTip);
        }
    }
}