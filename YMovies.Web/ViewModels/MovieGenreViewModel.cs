﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.ViewModels
{
    public class MovieGenreViewModel
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public string imDbRating { get; set; }
        
    }
}