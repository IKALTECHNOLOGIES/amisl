using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class Tipo
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string identificador { get; set; }
        [Required]
        public Int32 tipo1 { get; set; }
        [Required]
        public string nivel { get; set; }
        [Required]
        public Int32 ing_egr_est { get; set; }
        
    }
}