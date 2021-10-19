// ReSharper disable all

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Models;


namespace WebApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<DataModels.Playlist> GetPlaylistsAll() => _context.Playlists;

        [HttpGet("{roomId:int}")]
        public IEnumerable<DataModels.Track> GetPlaylist(int roomId) => _context.Tracks.Where(
            track => _context.PlaylistsToTracks.Where(pt => pt.PlaylistId == (_context.Rooms.Where(room => room.RoomId == roomId).Select(room => room.Playlist.PlaylistId).First()))
                .Select(pt => pt.TrackId).Contains(track.TrackId)).ToArray();

    }
}