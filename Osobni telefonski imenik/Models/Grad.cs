using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Grad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GradID { get; set; }

        public string Naziv { get; set; }

        public Guid DrzavaID { get; set; }

        public virtual Drzava Drzava { get; set; }
    }
}