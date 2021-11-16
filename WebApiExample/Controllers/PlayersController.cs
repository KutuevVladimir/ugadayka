// ReSharper disable all

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;



namespace WebApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DataModels.Player> GetPlayersAll() => _context.Players;

    }
}