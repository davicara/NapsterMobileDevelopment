using NapsterMobileDevelopment.Services;
using NapsterMobileDevelopment.Services.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;


namespace NapsterMobileDevelopment.Models
{

    public class Album
    {


        public string ID { get; set; }
        public string Title { get; set; }

        public List<Track> Tracks { get; set; }
        public string CoverArt {  get; set; }

        public Album(AlbumApiResponse apiResponse, string? image) 
        { 
            ID = apiResponse.ID;
            Title = apiResponse.Title;
            Tracks = apiResponse.Media[0].Tracks;

            if (image != null)
            {
                CoverArt = image;
            }
            else
            {
                CoverArt = "NapsterMobileDevelopment/Resources/ImageSource/album.png";
            }
        }

    }
}
