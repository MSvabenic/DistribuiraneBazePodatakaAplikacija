using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Grad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GradID { get; set; }

        [Required(ErrorMessage = "Obvezno je unijeti naziv grada!")]
        public string Naziv { get; set; }

        [DisplayName("Država")]
        [Required(ErrorMessage = "Obvezno je odabrati državu!")]
        public Guid DrzavaID { get; set; }

        public virtual Drzava Drzava { get; set; }
    }
}