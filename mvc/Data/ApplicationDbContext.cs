using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MVC.Models.Movie> Movie { get; set; }
        public DbSet<MVC.Models.Vehicle> Vehicle { get; set; }
        public DbSet<MVC.Models.Refuel> Refuel { get; set; }
        public DbSet<MVC.Models.Pouch> Pouch { get; set; }
        public DbSet<MVC.Models.Dialysis> Dialysis { get; set; }
    }
}
