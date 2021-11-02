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
    [ApiController]
    [Route("[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public TracksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public record TrackDescription(int Id, string Name, string author, string url);
        
        [HttpGet]
        public IEnumerable<TracksController.TrackDescription> GetAllTracks() => _context.Tracks.Select(track => new TracksController.TrackDescription(track.TrackId, track.Name, track.Author, track.Url)).ToArray();
        
        [HttpPost]
        public async Task<HttpResponseMessage> Post(DataModels.Track[] in_track_list)
        {

            foreach (DataModels.Track track in in_track_list)
            {
                _context.Tracks.Add(track);
            }
            await _context.SaveChangesAsync();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}