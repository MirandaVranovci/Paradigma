﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eStoreEntities : DbContext
    {
        public eStoreEntities()
            : base("name=eStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<KARTELA> KARTELAs { get; set; }
        public virtual DbSet<KATEGORIA> KATEGORIAs { get; set; }
        public virtual DbSet<KTHIMET_POROSIA> KTHIMET_POROSIA { get; set; }
        public virtual DbSet<LLOJI_PAGESES> LLOJI_PAGESES { get; set; }
        public virtual DbSet<LOG> LOGs { get; set; }
        public virtual DbSet<LOG_ENTRY> LOG_ENTRY { get; set; }
        public virtual DbSet<LOG_USERACTIVITY> LOG_USERACTIVITY { get; set; }
        public virtual DbSet<MEDIAT> MEDIATs { get; set; }
        public virtual DbSet<NJOFTIMET> NJOFTIMETs { get; set; }
        public virtual DbSet<NUMRAT> NUMRATs { get; set; }
        public virtual DbSet<PERDORUESI> PERDORUESIs { get; set; }
        public virtual DbSet<PERDORUESI_GRUPI> PERDORUESI_GRUPI { get; set; }
        public virtual DbSet<POROSIA> POROSIAs { get; set; }
        public virtual DbSet<PRODHUESI> PRODHUESIs { get; set; }
        public virtual DbSet<PRODUKTI> PRODUKTIs { get; set; }
        public virtual DbSet<PRODUKTI_IN_KATEGORIA> PRODUKTI_IN_KATEGORIA { get; set; }
        public virtual DbSet<PRODUKTI_IN_MEDIA> PRODUKTI_IN_MEDIA { get; set; }
        public virtual DbSet<PRODUKTI_IN_POROSIA> PRODUKTI_IN_POROSIA { get; set; }
        public virtual DbSet<SLLIDE> SLLIDEs { get; set; }
        public virtual DbSet<STATUSI_PAGESES> STATUSI_PAGESES { get; set; }
        public virtual DbSet<STOKU> STOKUs { get; set; }
        public virtual DbSet<SYSMENU> SYSMENUs { get; set; }
        public virtual DbSet<SYSMENU_PARENT> SYSMENU_PARENT { get; set; }
    }
}
