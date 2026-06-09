using System;
using System.Collections.Generic;
using System.Text;

namespace NapsterMobileDevelopment.Models
{
    public class SearchViewModel
    {

        public string Image { get; set; }
        public string Name { get; set; }
        public string Extra { get; set; }
        public string Type { get; set; }
        public string ID { get; set; }

        public SearchViewModel(string type, string id="", string name="", string image = "/NapsterMobileDevelopment/Resources/Images/no_image.png", string extra = "")
        {
            Image = image;

            if (image == null || image == "")
            {
                Image = "/NapsterMobileDevelopment/Resources/Images/no_image.png";
            }

            Name = name;

            Extra = extra;
            Type = type;
            ID = id;
        }
    }
}
