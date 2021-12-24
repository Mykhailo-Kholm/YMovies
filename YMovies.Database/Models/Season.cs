using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }

        public int CurrentSeriesId { get; set; }
        public Series CurrentSeries { get; set; }

        public virtual ICollection<Liked> Liked { get; set; }
        public virtual ICollection<Watched> Watched { get; set; }
    }
}
