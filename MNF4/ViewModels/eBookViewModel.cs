using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MNF4.ViewModels
{
    public class ebookViewModel
    {
        public string _product { get; set; }
        public string _title { get; set; }
        public decimal _price { get; set; }

        public Uri bookURL { get; set; }
        public Uri audioURL { get; set; }
        public Uri previousURL { get; set; }


        public ebookViewModel(string product, string title, string amount)
        {
            decimal _parsedPrice;

            _product = product;
            _title = title;

            if (Decimal.TryParse(amount, NumberStyles.Currency, new CultureInfo("en-us"), out _parsedPrice))
                _price = _parsedPrice;
            else
                _price = Convert.ToDecimal(14.95);

            bookURL = BuildBookURL(_product);
            audioURL = BuildAudioURL(_product);

        }

        /// <summary>
        /// TODO: Do I want to somehow encode/encrypt the URL's for the media, so I don't have to change the filename on the server?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Uri BuildBookURL(string id)
        {
            Uri ebookURL = new Uri("http://www.marquettenutrition.com/Content/Documents/");
            UriBuilder uriBuilder = new UriBuilder(ebookURL);

            switch (id)
            {
                case "ebPCOS1":
                    uriBuilder.Path += "TheQuickStartGuideToPCOS.pdf";
                    break;
                case "ebPCOS2":
                    uriBuilder.Path += "TheNutritionGuideToPCOS.pdf";
                    break;
                case "ebPCOS3":
                    uriBuilder.Path += "TheSupplementGuideToPCOS.pdf";
                    break;
                case "ebCeliacBC":
                    uriBuilder.Path += "CeliacBootcamp.pdf";
                    break;
            }
            return uriBuilder.Uri;
        }

        private Uri BuildAudioURL(string id)
        {
            Uri audioBookURL = new Uri("http://www.marquettenutrition.com/Content/Media/");
            UriBuilder uriBuilder = new UriBuilder(audioBookURL);
            
            switch (id)
            {
                case "ebPCOS1":
                    uriBuilder.Path += "TheQuickStartGuideToPCOS.mp3";
                    break;
                case "ebPCOS2":
                    uriBuilder.Path += "TheNutritionGuideToPCOS.mp3";
                    break;
                case "ebPCOS3":
                    uriBuilder.Path += "TheSupplementGuideToPCOS.mp3";
                    break;
                case "ebCeliacBC":
                    uriBuilder.Path += "CeliacBootcamp.mp3";
                    break;
            }
            return uriBuilder.Uri;
        }

    }
}