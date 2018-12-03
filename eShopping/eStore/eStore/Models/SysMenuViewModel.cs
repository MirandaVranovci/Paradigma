using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace eStore.Models
{
    public class SysMenuViewModel
    {
        public int IDP { get; set; }

         

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

        public string PershkrimiP { get; set; }

        public string HtmlKlasaP { get; set; }

        public int RoliIDP { get; set; }

        public int RenditjaP { get; set; }


        public SYSMENU_PARENT sp { get; set; } 

        public List<SYSMENU_PARENT> sysparenti { get; set; }

        public SYSMENU s { get; set; }

        public List<SYSMENU> sys { get; set; }
        public virtual SYSMENU_PARENT SYSMENU_PARENT { get; set; }

    }
    public class EditMenuViewModel
    {
        public EditMenuViewModel()
        { 
            sysmenu = new SYSMENU();
            parenti = new SYSMENU_PARENT();
        }

       
        public SYSMENU sysmenu { get; set; }

        public SYSMENU_PARENT parenti { get; set; }

        public int ID { get; set; } 
    }
}