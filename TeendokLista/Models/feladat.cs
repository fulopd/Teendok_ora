//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeendokLista.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class feladat
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public System.DateTime LetrehozasDatum { get; set; }
        public string Szoveg { get; set; }
        public bool Elvegezve { get; set; }
        public int felhasznaloId { get; set; }
    
        public virtual felhasznalo felhasznalo { get; set; }

    }
}
