﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    public class Series
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeriesId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Year { get; set; }
        public string Plot { get; set; }
        public decimal Budget { get; set; }
        public string BoxOffice { get; set; }
        public decimal ImdbRating { get; set; }

        public ICollection<Season> Seasons { get; set; }
        public virtual Statistic Statistic { get; set; }
        public virtual ICollection<Cast> Cast { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}