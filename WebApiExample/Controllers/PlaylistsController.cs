// ReSharper disable all

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;


namespace WebApiExample.Controllers
{
    public class PlaylistJson
    {
        public int PlaylistId { get; set; }

        public string Name { get; set; }

        public string PlayerId { get; set; }
        
        public List<int> trackIds { get; set; }
    }
    
    [ApiController]
    [Route("[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private static readonly HttpClient client = new HttpClient();

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

        [HttpPost]
        public async Task<HttpResponseMessage> Post(PlaylistJson inPlayList)
        {
            List<DataModels.Track> trackList =
                _context.Tracks.Where(t => inPlayList.trackIds.Contains(t.TrackId)).ToList();
            var player = _context.Players.First(p => p.PlayerId.Equals(inPlayList.PlayerId));
            DataModels.Playlist playlist = new DataModels.Playlist
            {
                Name = inPlayList.Name,
                Player = player,
                PlaylistsToTracks = new List<DataModels.PlaylistsToTracks>()
            };
            _context.Playlists.Add(playlist);
            foreach (DataModels.Track track in trackList)
            {
                playlist.PlaylistsToTracks.Add(new DataModels.PlaylistsToTracks{Playlist = playlist, Track = track});
            }
            await _context.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}