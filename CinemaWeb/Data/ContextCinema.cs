using CinemaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Data
{
    public class ContextCinema : DbContext
    {
        public ContextCinema(DbContextOptions options) : base(options) { }

        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Theater> Theaters { get; set; }
    }
}
