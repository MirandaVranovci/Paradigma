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
    
    public partial class PRODUKTI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUKTI()
        {
            this.PRODUKTI_DETAJET = new HashSet<PRODUKTI_DETAJET>();
            this.PRODUKTI_IN_KATEGORIA = new HashSet<PRODUKTI_IN_KATEGORIA>();
            this.PRODUKTI_IN_MEDIA = new HashSet<PRODUKTI_IN_MEDIA>();
            this.PRODUKTI_IN_POROSIA = new HashSet<PRODUKTI_IN_POROSIA>();
            this.STOKUs = new HashSet<STOKU>();
        }
    
        public int ID { get; set; }
        public string Emertimi { get; set; }
        public string Pershkrimi { get; set; }
        public int GjuhaID { get; set; }
    
        public virtual GJUHA GJUHA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUKTI_DETAJET> PRODUKTI_DETAJET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUKTI_IN_KATEGORIA> PRODUKTI_IN_KATEGORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUKTI_IN_MEDIA> PRODUKTI_IN_MEDIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUKTI_IN_POROSIA> PRODUKTI_IN_POROSIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STOKU> STOKUs { get; set; }
    }
}
