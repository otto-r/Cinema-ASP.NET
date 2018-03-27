using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public Movie Movie { get; set; }
        public List<Patron> Patrons { get; set; }   //Max patrons depends on seats in Theater
        public Theater Theater { get; set; }

        public Show(DateTime timeStart, Movie movie, Theater theater)
        {
            TimeStart = timeStart;
            Movie = movie;
            TimeEnd = TimeStart + Movie.RunningTime;
            Theater = theater;
            Patrons = new List<Patron>();
        }

        public Show()
        {

        }

        [NotMapped, Required, Range(1, 12)]
        public int bookingInteger { get; set; }

        public void BookSeats([Required, Range(1, 12)] int seatsRequested)
        {
            if ((Theater.TheaterCapacity - Patrons.Count) > seatsRequested)
            {
                for (int i = 0; i < seatsRequested; i++)
                {
                    Patrons.Add(new Patron());
                }
            }
        }

        public int SeatsLeft(int patronsBooked, int seatsTotal)
        {
            return seatsTotal - patronsBooked;
        }

        public int SeatsLeftNew()
        {
            return (Theater.TheaterCapacity - Patrons.Count);
        }
    }
}
