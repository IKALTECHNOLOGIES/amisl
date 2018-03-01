using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class IngresoRepFinanciero
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public int mes { get; set; }
        [Required]
        public int anio { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public string nivel { get; set; }
        [Required]
        public decimal saldoanterior { get; set; }
        [Required]
        public decimal tasacambio { get; set; }
        
        
    }
}