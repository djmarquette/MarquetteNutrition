using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNF4.ViewModels
{
    public class ProductViewModel
    {
        public string StorePage { get; set; }
        public string Command { get; set; }

        public ProductViewModel()
        {
            StorePage = null;
            Command = null;
        }

        public ProductViewModel(string _storePage)
        {
            StorePage = _storePage;
            Command = null;
        }
    }

}