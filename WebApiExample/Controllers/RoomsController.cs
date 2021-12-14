// ReSharper disable all

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebApiExample.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase

    {

        public record RoomDescription(int Id, string Name, int PlayersCount, int MaxPlayersCount, bool RequiresPassword);

        public record PlayerWrapper(string UserId, string NickName);

        public record RoomWrapper(RoomDescription Description, PlayerWrapper[] Players, DataModels.Track[] songs);

        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<RoomDescription> GetAll() => _context.Rooms.Select(room => new RoomDescription(room.RoomId, room.Name, room.Players.Count, room.MaxPlayers, room.RequiresPassword)).ToArray();

        [HttpGet("{roomId:int}")]
        public RoomWrapper Get(int roomId)
        {
            var room = _context.Rooms.Where(room => room.RoomId == roomId).Include(room => room.Players).First();

            PlayerWrapper[] players = room.Players.Select(player => new PlayerWrapper(player.PlayerId,player.DisplayName)).ToArray();

            var roomDescriprion =
                new RoomDescription(room.RoomId, room.Name, players.Length, room.MaxPlayers, room.RequiresPassword);


            DataModels.Track[] tracks = _context.Tracks.Where(
                track => _context.PlaylistsToTracks.Where(pt =>
                        pt.PlaylistId == (_context.Rooms.Where(room => room.RoomId == roomId).Select(room =>
                            room.Playlist.PlaylistId).First()
                        ))
                    .Select(pt => pt.TrackId).Contains(track.TrackId)).ToArray();

            return new RoomWrapper(roomDescriprion, players, tracks);

        }
        
        [HttpPost("addroom")]
        public async Task<string> RoomCreate([FromBody] DataModels.Room myRoom)
        {
            //TODO: Validate input
            _context.Rooms.Add(myRoom);
            /*
               {
                  "name": "string",
                  "image": null,
                  "state": true,
                  "maxPlayers": 5,
                  "requiresPassword": false,
                  "password": null,
                  "player": null,
                  "playlist": null,
                  "players": null
                }
             */
            await _context.SaveChangesAsync();
            return myRoom.RoomId.ToString();
        }
        public record RoomPlayer(int idRoom, string idPlayer, int is_remove);
        
        [HttpPost("addplayer")]
        public async Task<string> AddPlayerToRoom([FromBody] RoomPlayer request)
        {
            string idPlayer = request.idPlayer;
            int idRoom = request.idRoom; 
            DataModels.Player p = _context.Players.Where(pl => pl.PlayerId == idPlayer).First();
            DataModels.Room r = _context.Rooms.Where(room => room.RoomId == idRoom).Include(room => room.Players).First();
            if (r != null && p != null)
            {
                if (request.is_remove == 1)
                    r.Players.Remove(p);
                else
                    r.Players.Add(p);
            }
            else
            {
                return "Not found";
            }
            await _context.SaveChangesAsync();
            return "OK";
        }        
    }
}
