using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.ViewModels
{
    public class WebinarViewModel
    {
        public string StorePage { get; set; }
        public string Command { get; set; }

        public WebinarViewModel()
        {
            StorePage = null;
            Command = null;
        }
    }

}