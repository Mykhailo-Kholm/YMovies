﻿using System.Collections.Generic;

namespace YMovies.Web.Models.AdminViewModels
{
    public class ManageUserRightsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}