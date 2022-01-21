using System.Collections.Generic;

namespace YMovies.Web.Models.AboutUs
{
    public class AboutUsModel
    {
        public string Name { get; set; }
        public string SureName { get; set; }
        public List<string> ResponsobilitiesList { get; set; }
        public string Position { get; set; }
        public string GeneralInfo { get; set; }
        public string ImgUrl { get; set; }
        public string LinkedInUrl { get; set; }

    }
}