//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEDIAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEDIAT()
        {
            this.PRODUKTI_IN_MEDIA = new HashSet<PRODUKTI_IN_MEDIA>();
            this.SLLIDEs = new HashSet<SLLIDE>();
        }
    
        public int ID { get; set; }
        public string Pershkrimi { get; set; }
        public System.Guid Emri { get; set; }
        public int Extensioni { get; set; }
        public string Shtegu { get; set; }
        public System.DateTime Created { get; set; }
        public int Createdby { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUKTI_IN_MEDIA> PRODUKTI_IN_MEDIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SLLIDE> SLLIDEs { get; set; }
    }
}
