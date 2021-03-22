using InclusMap.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Services.Data
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Place> Places { get; set; }

        public DbSet<PlaceLinks> PlacesLinks { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
           
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaceLinks>().HasKey(new string[] { "FromId", "ToId" });
            modelBuilder.Entity<Place>()
                .HasMany<PlaceLinks>(q => q.Links)
                .WithOne()
                .HasForeignKey(z => z.FromId);

        }
    }
}
