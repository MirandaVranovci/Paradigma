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
    
    public partial class SYSMENU
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string ActionName { get; set; }
        public string Controller { get; set; }
        public string Pershkrimi { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string HtmlClass { get; set; }
        public Nullable<int> Renditja { get; set; }
        public string OpParamName { get; set; }
        public string OpParamValue { get; set; }
        public string styleid { get; set; }
    
        public virtual SYSMENU_PARENT SYSMENU_PARENT { get; set; }
    }
}