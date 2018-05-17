using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Osoba
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OsobaID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Opis { get; set; }

        public Grad GradID { get; set; }

        public virtual Grad Grad{ get; set; }
    }
}