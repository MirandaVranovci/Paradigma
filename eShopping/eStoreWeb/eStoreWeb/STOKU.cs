//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eStoreWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class STOKU
    {
        public int ID { get; set; }
        public int ArtikulliID { get; set; }
        public int Sasia { get; set; }
    
        public virtual PRODUKTI PRODUKTI { get; set; }
    }
}
