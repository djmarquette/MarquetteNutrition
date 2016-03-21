using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.ViewModels
{
    public class StoreViewModel
    {
        public string StorePage { get; set; }

        public StoreViewModel()
        {
            StorePage = "https://www.marquettenutrition.com/store";
        }
    }
}