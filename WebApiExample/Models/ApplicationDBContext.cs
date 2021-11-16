using System;
using Microsoft.EntityFrameworkCore;

namespace WebApiExample.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
#endif
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataModels.PlaylistsToTracks>().HasKey(sc => new { sc.TrackId, sc.PlaylistId });
            modelBuilder.Entity<DataModels.PlaylistsToLikes>().HasKey(sc => new { sc.PlayerId, sc.PlaylistId });
        }

        public DbSet<DataModels.Player> Players { get; set; }
        public DbSet<DataModels.Track> Tracks { get; set; }
        public DbSet<DataModels.Playlist> Playlists { get; set; }
        public DbSet<DataModels.Room> Rooms { get; set; }

        public DbSet<DataModels.PlaylistsToTracks> PlaylistsToTracks { get; set; }

        public DbSet<DataModels.PlaylistsToLikes> PlaylistsToLikes { get; set; }

    }
}