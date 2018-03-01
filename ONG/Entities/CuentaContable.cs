using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class CuentaContable
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string codigo { get; set; }
        [Required]
        public string identificador { get; set; }
        [Required]
        public string tipo { get; set; }
        [Required]
        public string nivel { get; set; }
        
    }
}