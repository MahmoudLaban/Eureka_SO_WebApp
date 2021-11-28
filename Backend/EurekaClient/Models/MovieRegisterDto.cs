﻿using System.ComponentModel.DataAnnotations;

namespace EurekaClient.Models
{

    public class MovieRegisterDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        public int Year { get; set; }
    }
}
