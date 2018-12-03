using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MessageJS
    {
        public int ID { get; set; }
        public string Model { get; set; }

        public bool Status { get; set; }
        public string Message { get; set; }

        public int? UserID { get; set; }

        public bool editing { get; set; }

        public int? KontrataID { get; set; }

        public bool anyadded { get; set; }
        public decimal Shuma { get; set; }
    }
}
