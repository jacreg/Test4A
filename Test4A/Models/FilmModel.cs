using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test4A.Models
{
    public class FilmModel
    {
        public int index { get; set; }
        public int episode_id { get; set; }
        public string title { get; set; }

        public string opening_crawl { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string release_date { get; set; }

        public FilmScoreModel score { get; set; }
    }
}
