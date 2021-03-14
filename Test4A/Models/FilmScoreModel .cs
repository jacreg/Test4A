using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test4A.Models
{
    public class FilmScoreModel
    {
        [Key]
        public int episode_id { get; set; }
        public int count { get; set; }
        public int sum { get; set; }

        public string score => count == 0 ? "None" : Math.Round((decimal)sum / (decimal)count, 2, MidpointRounding.AwayFromZero).ToString("0.00");

    }
}
