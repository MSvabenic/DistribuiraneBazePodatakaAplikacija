using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class BrojTip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BrojTipID { get; set; }

        public string Naziv { get; set; }
    }
}