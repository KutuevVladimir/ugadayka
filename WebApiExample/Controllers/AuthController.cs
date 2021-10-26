// ReSharper disable all

using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNetCore.Mvc;
using WebApiExample.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

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

        private string GetId(string accessToken) {
            var uri = "https://www.googleapis.com/userinfo/v2/me?access_token=" + accessToken;
            var jsonStr = client.GetStringAsync(uri).Result;
            var js = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            return (string)js.id;
        }

        private string CodeAcceptorViaGoogleApi(string code) {
            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
            });
            var rec = apiCodeFlow.ExchangeCodeForTokenAsync("1", code, "http://localhost:3000", new System.Threading.CancellationToken()).Result;
            return GetId(rec.AccessToken);
        }

        private string CodeAcceptorViaPostRequest(string code) {
            var values = new Dictionary<string, string> {
                {"code", code},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"redirect_uri", "http://localhost:3000"},
                {"grant_type", "authorization_code"}
            };

            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("https://www.googleapis.com/oauth2/v4/token", content).Result;
            var jsonStr = response.Content.ReadAsStringAsync().Result;
            var js = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            return GetId((string)js.access_token);
        }

        [HttpPost("google_code")]
        public string CodeAcceptor([FromBody] string code) {
            return CodeAcceptorViaPostRequest(code);
            // return CodeAcceptorViaGoogleApi(code);
        }

        [HttpPost("google")]
        public string LoginOrRegister([FromBody] string tokenId) {
            var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
            return payload.Subject;
        }

    }
}