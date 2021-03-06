﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class OsobaBroj
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public Guid OsobaID { get; set; }

        public Guid BrojID { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual Brojevi Broj { get; set; }
    }
}