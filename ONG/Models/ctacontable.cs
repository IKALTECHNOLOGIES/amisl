//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ONG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ctacontable
    {
        public ctacontable()
        {
            this.gasto = new HashSet<gasto>();
            this.ingreso = new HashSet<ingreso>();
        }
    
        public string UUID { get; set; }
        public string CODIGO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string TIPO { get; set; }
        public string NIVEL { get; set; }
    
        public virtual ICollection<gasto> gasto { get; set; }
        public virtual ICollection<ingreso> ingreso { get; set; }
    }
}
