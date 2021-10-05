// ReSharper disable all

using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Controllers
{
    public record RoomDescription(int Id, string Name, int PlayersCount, int MaxPlayersCount, bool RequiresPassword);

    public record Player(int UserId, string NickName);

    public record Room(RoomDescription Description, Player[] Players, string[] Songs);

    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private static readonly Room[] Rooms =
        {
            new(new(0, "Угадай мелодию c F#", 5, 10, true),
                new[] { new Player(1, "Alex Berezhnykh") },
                new[] { "Гой еси, Нойзс МС", "Любимая песня твоей сестры" }),

            new(new(1, "Бременские музыканты", 10, 10, false),
                new[] { new Player(1, "Alex Berezhnykh") },
                new[] { "Гой еси, Нойзс МС", "Любимая песня твоей сестры" }),

            new(new(2, "Беременные практиканты", 1, 10, false),
                new[]
                {
                    new Player(1, "DedSec256"),
                    new Player(2, "Чебуратор5000"),
                    new Player(3, "Николай Басков"),
                    new Player(4, "Оксана Миронова"),
                },
                new[] { "Гой еси, Нойзс МС", "Любимая песня твоей сестры", "Breaking the waves", "Cadillac" })
        };

        [HttpGet]
        public RoomDescription[] GetAll() => Rooms.Select(t => t.Description).ToArray();

        [HttpGet("{roomId:int}")]
        public Room Get(int roomId) => Rooms[roomId];
    }
}
