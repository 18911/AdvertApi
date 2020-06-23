using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Requests;
using AdvertApi.Responses;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertApi.Controllers{
    [Route("api/advert")]
    [ApiController]
    public class AdvertApiController : ControllerBase{

        private readonly IApiDbService DbService;
        public IConfiguration configuration;

        public AdvertApiController(IApiDbService DbService, IConfiguration configuration){
            this.DbService = DbService;
            this.configuration = configuration;
        }

        public AdvertApiController(IApiDbService DbService){
            this.DbService = DbService;
        }

        [Route("api/advert/new-campaign")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateNewCampaign(NewCampaignRequest request) {
            List<Building> ListBuilings = DbService.CheckBuildings(request);
            if (ListBuilings != null){
                ListBuilings = DbService.GetBuildings(ListBuilings);
                NewCampaignResponse campaignResponse = CalculateService.GeCampaignSetup(ListBuilings, request.PricePerSquareMeter);
                var campaign = DbService.CreateNewCampaign(request);
                DbService.CreateNewBanners(campaign.IdCampaign, campaignResponse);
                campaignResponse.Campaign = campaign;
                return CreatedAtAction("CreateNewCampaign", campaignResponse);
            }
            return BadRequest("Nie poprawne dane budynków");
        }

        [Route("api/advert/campaigns")]
        [HttpGet]
        [Authorize]
        public IActionResult GetCampaigns(){
            List<CampaignsResponse> list = DbService.GetCampaigns();
            if (list != null) {
                return Ok(list);
            }
            return NotFound("Brak kampanii w bazie danych");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Register(Client client){
            Console.WriteLine(client);
            if (ModelState.IsValid) {
                client = DbService.Register(client);
                if (client == null) {
                    return BadRequest("Klient juz istnieje w bazie");
                }
                else{
                    return CreatedAtAction("Register", client);
                }
            }
            return BadRequest("Dane nie sa poprawne");
        }

        [Route("api/advert/login")]
        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest) {
            Client client = DbService.Login(loginRequest);
            if (client != null){
                var AccesToken = GetClentTokenData(client);
                var RefreshToken = GetRefreshToken();
                DbService.UpdateToken(client, RefreshToken.ToString());

                return Ok(new { AccesToken, RefreshToken });
            }

            return NotFound("Niepoprawne dane logowania");
        }

        [Route("api/advert/refresh-token/{token}")]
        [HttpPost]
        public IActionResult RefreshToken(string token){
            Client client = DbService.RefreshToken(token);
            if (client != null){
                var AccesToken = GetClentTokenData(client);
                var RefreshToken = GetRefreshToken();
                DbService.UpdateToken(client, RefreshToken.ToString());

                return Ok(new { AccesToken, RefreshToken });
            }

            return NotFound("Nieznaleziono podanego tokenu");
        }

        public string GetClentTokenData(Client client){
            Claim[] claims;
            JwtSecurityToken token = new JwtSecurityToken();

            claims = new[] {
                new Claim(
                    ClaimTypes.NameIdentifier,client.Login,
                    ClaimTypes.Role, "Client")
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token = new JwtSecurityToken(
                issuer: "Api",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Guid GetRefreshToken() {
            return Guid.NewGuid();
        }
    }
}