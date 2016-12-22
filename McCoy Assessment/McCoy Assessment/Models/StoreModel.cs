using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McCoy_Assessment.Models
{
    public class StoreModel
    {
        //Store Number
        public int? StoreNumber { get; set; }

        //Store Name
        public string StoreName { get; set; }

        //Store Manager Name
        public string StoreManagerName { get; set; }

        //Opening Time
        public DateTime? OpenTime { get; set; }

        //Closing Time
        public DateTime? CloseTime { get; set; }

    }
}