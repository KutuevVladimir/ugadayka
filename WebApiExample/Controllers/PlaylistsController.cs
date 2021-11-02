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

        public string Image { get; set; }

        public int PlayerId { get; set; }
        
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
        public async Task<HttpResponseMessage> Post(PlaylistJson in_play_list)
        {
            int playlist_id = _context.Playlists.Max(p => p.PlaylistId) + 1;
            List<DataModels.Track> track_list =
                _context.Tracks.Where(t => in_play_list.trackIds.Contains(t.TrackId)).ToList();
            DataModels.Playlist playlist = new DataModels.Playlist
            {
                PlaylistId = playlist_id,
                Name = in_play_list.Name,
                Image = Encoding.ASCII.GetBytes(in_play_list.Image),
                Player = _context.Players.First(p=>p.PlayerId == in_play_list.PlayerId),
                PlaylistsToTracks = new List<DataModels.PlaylistsToTracks>()
            };
            foreach (DataModels.Track track in track_list)
            {
                playlist.PlaylistsToTracks.Add(new DataModels.PlaylistsToTracks{PlaylistId = playlist_id,TrackId = track.TrackId});
            }
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}