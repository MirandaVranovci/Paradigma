using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStore.Models
{
    public class UploadsDTO
    {
        public int ID { get; set; }  
        public string Data { get; set; }
        public string Emri { get; set; }
        public string Tipi { get; set; }
        public string Shtegu { get; set; }
        public bool Status { get; set; }

        public string Pershkrimi { get; set; }
    }
}