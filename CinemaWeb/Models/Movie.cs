using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public TimeSpan RunningTime { get; set; }

        public Movie()
        {

        }
        public Movie(string name, TimeSpan runningTime)
        {
            Name = name;
            RunningTime = runningTime;
        }
    }
}
