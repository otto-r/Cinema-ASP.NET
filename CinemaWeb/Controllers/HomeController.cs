using CinemaWeb.Data;
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

            //var query = from show in list
            //            group show by show.TimeStart.Date into groupDate
                        //select groupDate;

            return View(list);
        }

        public async Task<IActionResult>Test()
        {
            //var list = await contextCinema.Movies.ToListAsync();

            var tt = contextCinema.Shows.Include(s => s.Movie).Include(s => s.Theater).ToList();

            return View(tt);    
        }
    }
}
