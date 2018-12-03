using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
   public class Enums
    {
        public enum FileType
        {
            [Description("Powerpoint")]
            ppt = 0,
            [Description("PDF")]
            pdf = 1,
            [Description("Image")]
            image = 2,
            [Description("Audio")]
            audio = 3,
            [Description("Other")]
            other = 4,
        }

        public enum ProduktiStatus
        {
            [Description("Draft")]
            Draft = 1,
            [Description("Aktive")]
            Aktive = 2
        }

        public enum NumratRendorLloji
        {
            [Description("Produkt")]
            Produkt = 1,
           
        }

    }
}
