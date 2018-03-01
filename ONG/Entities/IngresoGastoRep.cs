using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class IngresoGastoRep
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public decimal monto { get; set; }
        [Required]
        public string uuid_ingreso { get; set; }
        [Required]
        public string uuid_rep_financiero { get; set; }
        [Required]
        public string uuid_gasto { get; set; }
        [Required]
        public string uuid_estadistica { get; set; }
        [Required]
        public decimal valor { get; set; }
        
        
    }
}