﻿using System.Collections.Generic;
using YMovies.Identity.Dtos;

namespace YMovies.Web.Models.AdminViewModels
{
    public class ManageUsersViewModel
    {
        IEnumerable<UserDto> Users { get; set; }
    }
}