using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Brojevi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BrojID { get; set; }

        public Guid OsobaID { get; set; }

        public Guid BrojTipID { get; set; }

        public string Broj { get; set; }

        public string Opis { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual BrojTip BrojTip { get; set; }
    }
}