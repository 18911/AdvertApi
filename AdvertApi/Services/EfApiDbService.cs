using AdvertApi.Models;
using AdvertApi.Requests;
using AdvertApi.Responses;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace projekt_test.Services{
    public class EfApiDbService : IApiDbService{
        private readonly ApiDbContext DbContext;

        public EfApiDbService(ApiDbContext DbContext){
            this.DbContext = DbContext;
        }

        [Obsolete]
        public Client Register(Client client){

            Console.WriteLine("Klient " + client.Login);

            if (DbContext.Client.Where(c => c.Login == client.Login).Any()) {
                client = null;
            }
            else{
                var MaxId = DbContext.Client.Max(c => c.IdClient);
                client.IdClient = MaxId + 1;
                var salt = CreateSalt();
                var password = Create(client.Password,salt);
                client.Password = password;
                client.Salt = salt;
                DbContext.Client.Add(client);
                DbContext.SaveChanges();
            }
            return client;
        }

        public string Create(string value,string salt) {
            var valueBytes = KeyDerivation.Pbkdf2(
                    password: value,
                    salt: Encoding.UTF8.GetBytes(salt),
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 256/8
                );
            return Convert.ToBase64String(valueBytes);
        }


        public string CreateSalt() {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create()){
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public bool Validate(string value, string salt, string hash) => Create(value, salt) == hash;

        public Client Login(LoginRequest loginRequest){
            Client client = new Client();
            if (DbContext.Client.Where(c => c.Login == loginRequest.Login).Any()) {
                client = DbContext.Client.SingleOrDefault(c => c.Login == loginRequest.Login);
                if (!Validate(loginRequest.Password,client.Salt, client.Password)) {
                    client = null;
                }
            }
            else {
                client = null;
            }
            return client;
        }

        public Client RefreshToken(string token){
            Client client = new Client();
            if (DbContext.Client.Where(c => c.RefreshToken == token).Any()) {
                client = DbContext.Client.SingleOrDefault(c => c.RefreshToken == token);
            }
            else {
                client = null;
            }
            return client;
        }

        public void UpdateToken(Client client, string token){
            client.RefreshToken = token;
            DbContext.SaveChanges();
        }

        public List<CampaignsResponse> GetCampaigns(){
            var result = from campaing in DbContext.Campaign
                         join client in DbContext.Client on campaing.IdClient equals client.IdClient
                         select new CampaignsResponse { Campaign = campaing,Client = client};
            
            List<CampaignsResponse> list = result.ToList();
            foreach (CampaignsResponse c in list) { 
                var r = from banner in DbContext.Banner
                         where banner.IdCampaing == c.Campaign.IdCampaign
                         select new Banner { IdAdvertisement = banner.IdAdvertisement,Name = banner.Name,Price = banner.Price,IdCampaing = banner.IdCampaing,Area = banner.Area};
                c.ListBanners = r.ToList();
            }

            if (list.Count() > 0) {
                list.OrderBy(a => a.Campaign.StartDate).Reverse().ToList();
            } else {
                list = null;
            }
            return list;
        }

        public List<Building> CheckBuildings(NewCampaignRequest CampaignRequest){
            var list = new List<Building>();
            if (DbContext.Building.Where(b=>b.IdBuilding == CampaignRequest.FromIdBuilding).Any() && DbContext.Building.Where(b => b.IdBuilding == CampaignRequest.ToIdBuilding).Any()) {
                var b1 = GetBuilding(CampaignRequest.FromIdBuilding);
                var b2 = GetBuilding(CampaignRequest.ToIdBuilding);
                if (b1.City == b2.City && b1.Street == b2.Street) {
                    list.Add(b1);
                    list.Add(b2);
                    return list;
                }
                list = null;
                return list;
            }
            list = null;
            return list;
        }

        public Building GetBuilding(int id) {
            return DbContext.Building.SingleOrDefault(b => b.IdBuilding == id);
        }

        public List<Building> GetBuildings(List<Building> buildings){
            var result = from building in DbContext.Building
                         where building.City == buildings[1].City && building.Street == buildings[1].Street && (building.StreetNumber < buildings[1].StreetNumber && building.StreetNumber > buildings[0].StreetNumber)
                         orderby building.StreetNumber ascending
                         select building;
            return result.ToList();
        }

        public Campaign CreateNewCampaign(NewCampaignRequest request){
            var Id = DbContext.Campaign.Max(c=>c.IdCampaign) + 1;
            var campaign = new Campaign {IdCampaign = Id,IdClient = request.IdClient,StartDate = request.StartDate,EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,FromIdBuilding = request.FromIdBuilding,ToldBuilding = request.ToIdBuilding                                    
            };
            DbContext.Campaign.Add(campaign);
            DbContext.SaveChanges();
            return campaign;
        }

        public void CreateNewBanners(int IdC,NewCampaignResponse campaignResponse){
            foreach (Banner b in campaignResponse.ListBanners) {
                var Id = DbContext.Banner.Max(b => b.IdAdvertisement) + 1;
                var banner = new Banner { IdAdvertisement = Id,Name = b.Name,Price = b.Price,IdCampaing = IdC,Area = b.Area};
                DbContext.Banner.Add(banner);
                DbContext.SaveChanges();
            }
        }
    }
}
