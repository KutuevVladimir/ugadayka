using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiExample.Models
{
    public class DataModels
    {
        public class Player
        {
            [Key]
            public string PlayerId { get; set; }

            public string DisplayName { get; set; }

            public string Email { get; set; }

            public int Rating { get; set; }

            public string Image { get; set; }
            
            public bool is_admin { get; set; }
            
            public ICollection<PlaylistsToLikes> PlaylistsToLikes { get; set; }
        }

        public class Track
        {
            [Key]
            public int TrackId { get; set; }

            public string Name { get; set; }

            public string Author { get; set; }

            public string Url { get; set; }

            public ICollection<PlaylistsToTracks> PlaylistsToTracks { get; set;}
        }

        public class Playlist
        {
            [Key]
            public int PlaylistId { get; set; }

            public string Name { get; set; }

            public Player Player { get; set; }

            public ICollection<PlaylistsToTracks> PlaylistsToTracks { get; set; }

            public ICollection<PlaylistsToLikes> PlaylistsToLikes { get; set; }
        }
        
        public class PlaylistsToTracks
        {
            public int PlaylistId { get; set; }
            public Playlist Playlist { get; set; }

            public int TrackId { get; set; }
            public Track Track { get; set; }
        }
        
        public class PlaylistsToLikes
        {
            public int PlaylistId { get; set; }
            public Playlist Playlist { get; set; }

            public string PlayerId { get; set; }
            public Player Player { get; set; }
        }

        public class Room
        {
            [Key]
            public int RoomId { get; set; }

            public string Name { get; set; }
            
            public byte[] Image { get; set; }
            
            public bool State { get; set; }
            
            public int MaxPlayers { get; set; }

            public bool RequiresPassword { get; set; }

            public string Password { get; set; }

            public Player Player { get; set; }
            
            public Playlist Playlist { get; set; }

            public ICollection<Player> Players { get; set; }
        }
        
    }
}