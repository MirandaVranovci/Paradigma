using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStore.Models
{
    public class GetUser
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public DateTime? Ditelindja { get; set; }
        public string Adresa { get; set; }
        public string Telefoni { get; set; }
        
        public int Gjuha { get; set; }
        public string Email { get; set; }
       
        public string Roles { get; set; }
        public DateTime DataPrej { get; set; }
        public DateTime? DataDeri { get; set; }


    }
}