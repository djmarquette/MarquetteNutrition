﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.Models
{
    public class PayPal
    {
        public PayPal()
        {
            
        }

        public string cmd { get; set; }
        public string business { get; set; }
        public string @return { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public string item_name { get; set; }
        public string amount { get; set; }

    }
}