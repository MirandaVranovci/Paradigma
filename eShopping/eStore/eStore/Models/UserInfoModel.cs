using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStore.Models
{
   
        public class UserInfoModel
        {
            public int ID { get; set; }

            public string Emri { get; set; }
            public string Roli { get; set; }
        }

        public class NotificationsModel
        {
            public int ID { get; set; }
            public string From { get; set; }
            public string Message { get; set; }
            public string Date { get; set; }


        }
    
}