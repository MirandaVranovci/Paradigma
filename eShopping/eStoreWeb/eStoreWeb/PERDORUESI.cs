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
    
    public partial class PERDORUESI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERDORUESI()
        {
            this.KARTELAs = new HashSet<KARTELA>();
            this.KTHIMET_POROSIA = new HashSet<KTHIMET_POROSIA>();
            this.NJOFTIMETs = new HashSet<NJOFTIMET>();
            this.NJOFTIMETs1 = new HashSet<NJOFTIMET>();
        }
    
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public Nullable<System.DateTime> Datelindja { get; set; }
        public byte[] Password { get; set; }
        public Nullable<System.DateTime> PerdoruesiFillimi { get; set; }
        public Nullable<System.DateTime> PerdoruesiMbarimi { get; set; }
        public bool Aktiv { get; set; }
        public string Telefoni { get; set; }
        public string Email { get; set; }
        public string KodiPostar { get; set; }
        public string Adresa { get; set; }
        public int GjuhaID { get; set; }
        public string Roles { get; set; }
        public Nullable<bool> Statusi { get; set; }
    
        public virtual GJUHA GJUHA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KARTELA> KARTELAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KTHIMET_POROSIA> KTHIMET_POROSIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NJOFTIMET> NJOFTIMETs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NJOFTIMET> NJOFTIMETs1 { get; set; }
    }
}
