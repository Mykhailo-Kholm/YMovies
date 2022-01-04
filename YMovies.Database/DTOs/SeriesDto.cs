﻿using System.Collections.Generic;

namespace YMovies.Database.DTOs
{
    class SeriesDto
    {
        public int SeriesId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Year { get; set; }
        public string Plot { get; set; }
        public decimal Budget { get; set; }
        public string BoxOffice { get; set; }
        public decimal ImdbRating { get; set; }

        public  ICollection<SeasonDto> Seasons { get; set; }
        public StatisticDto Statistic { get; set; }
        public ICollection<CastDto> Cast { get; set; }
        public ICollection<CountryDto> Countries { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
    }
}