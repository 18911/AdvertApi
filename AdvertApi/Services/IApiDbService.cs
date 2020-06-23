using AdvertApi.Models;
using AdvertApi.Requests;
using AdvertApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt_test.Services{
    public interface IApiDbService{
        public Client Register(Client client);
        public Client Login(LoginRequest loginRequest);

        public Client RefreshToken(string token);

        public void UpdateToken(Client client,string token);

        public List<CampaignsResponse> GetCampaigns();

        public List<Building> CheckBuildings(NewCampaignRequest CampaignRequest);

        public Building GetBuilding(int id);

        public List<Building> GetBuildings(List<Building> buildings);

        public Campaign CreateNewCampaign(NewCampaignRequest CampaignRequest);
        public void CreateNewBanners(int IdCampaign,NewCampaignResponse campaignResponse);
    }
}
