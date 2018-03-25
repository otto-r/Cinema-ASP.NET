using System;
using System.Collections.Generic;
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

        //Solve to see if theater is full. With a function maybe?
        public Show()
        {

        }

        public void BookSeats(List<Patron> requestSeatsList)
        {
            if ((Theater.TheaterCapacity - Patrons.Count) > requestSeatsList.Count)
            {
                foreach (var patron in requestSeatsList)
                {
                    Patrons.Add(patron);
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
