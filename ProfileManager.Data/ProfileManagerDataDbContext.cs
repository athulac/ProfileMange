using Microsoft.EntityFrameworkCore;
using ProfileManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProfileManager.Data
{
    public class ProfileManagerDataDbContext : DbContext
    {
        public ProfileManagerDataDbContext(DbContextOptions<ProfileManagerDataDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Family> Families { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        // base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<School>().HasData(
        //          new School { Id = 1, Name = "maliyadeva", Address = "kurunegala" },
        //          new School { Id = 2, Name = "presedents", Address = "panadura" });
        //}

    }
}
