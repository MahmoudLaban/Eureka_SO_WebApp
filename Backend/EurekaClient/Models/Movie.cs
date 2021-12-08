using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EurekaClient.Models
{
    //For retrieving movies
    public class Movie
    {
        public int id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

    }
}
