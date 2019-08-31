using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PolygonAPI.Models
{
    public partial class MapPolygonContext : DbContext
    {
        public MapPolygonContext()
        {
        }

        public MapPolygonContext(DbContextOptions<MapPolygonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Polygons> Polygons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("PolyMapConenctionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Polygons>(entity =>
            {
                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.LocationCoordinates).IsRequired();
            });
        }
    }
}
