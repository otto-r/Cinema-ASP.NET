using CinemaWeb.Data;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextCinema contextCinema;
        public HomeController(ContextCinema context)
        {
            contextCinema = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).Include(s => s.Patrons).ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Test()
        {
            //var list = await contextCinema.Movies.ToListAsync();

            var tt = contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).ToList();

            return View(tt);
        }

        [HttpGet]
        public async Task<IActionResult> Booking(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var show = await contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).Include(s => s.Patrons).SingleAsync(r => r.Id == id);

            if (show == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(show);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Booking(int? id, string numberOfTickets)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var show = await contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).Include(s => s.Patrons).SingleAsync(r => r.Id == id);

            if (show == null)
            {
                return RedirectToAction(nameof(Index));
            }

            int tickets;
            try
            {
                tickets = int.Parse(numberOfTickets);
            }
            catch (Exception exc)
            {
                return RedirectToAction(nameof(Index));
            }

            int movieId = show.Movie.Id;

            if (tickets > 0 && tickets <= 12 && tickets <= show.SeatsLeftNew())
            {
                for (int i = 0; i < tickets; i++)
                {
                    show.Patrons.Add(new Patron());
                }
                contextCinema.Update(show);
                await contextCinema.SaveChangesAsync();


                return RedirectToAction(nameof(Confirmation), new { movieId, tickets });
            }
            else

            return View(show);
        }

        public async Task<IActionResult> Confirmation(int movieId, int tickets)
        {
            //var movie = await contextCinema.Movies.SingleAsync(m => m.Id == movieId);

            var show = await contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).Include(s => s.Patrons).FirstOrDefaultAsync(r => r.Movie.Id == movieId);


            string movieName = show.Movie.Name;

            ViewBag.tickets = tickets;
            ViewBag.movie = movieName;


            return View(show);
        }
    }
}