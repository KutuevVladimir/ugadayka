// ReSharper disable all

using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace WebApiExample.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly string clientId = "310235818115-ebt6pip8ien918kf1kopbsi0hroe1v7q.apps.googleusercontent.com";
        private readonly string clientSecret = "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";
        private static readonly HttpClient client = new HttpClient();
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("google")]
        public string LoginOrRegister([FromBody] string tokenId) {
            var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
            var old = _context.Players.Find(payload.Subject);
            if (old == null)
            {
                _context.Players.Add(new DataModels.Player()
                {
                    PlayerId = payload.Subject,
                    Email = payload.Email,
                    Image = payload.Picture,
                    is_admin = false,
                    DisplayName = payload.Name,
                    Rating = 0,
                });
            }
            else
            {
                old.Image = payload.Picture;
            }
            _context.SaveChanges();

            return payload.Subject;
        }

    }
}