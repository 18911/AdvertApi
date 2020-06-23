using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AdvertApi.Models;
using AdvertApi.Controllers;
using AdvertApi.Services;
using AdvertApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApiTests.UnitTests.AdvertApi{
    
    [TestFixture]
    public class AdvertApiCampaignsUnitTest{

        [Test]
        public void GetCampaignsMethod_MoreThanOneCampaign_Correct() {
            var DbLayer = new Mock<IApiDbService>();

            DbLayer.Setup(d => d.GetCampaigns()).Returns(new List<CampaignsResponse>() {
                new CampaignsResponse{ Campaign = new Campaign { IdCampaign = 1, IdClient = 1, StartDate = DateTime.Parse("2020-3-1"), EndDate = DateTime.Parse("2020-3-29"), PricePerSquareMeter = 5000.0m, FromIdBuilding = 1, ToldBuilding = 2 },
                    Client=new Client{ IdClient = 1, FirstName = "Jan", LastName = "A", Email = "Jan@maiil.com", Phone = "100 200 300", Login = "JanA", Password = "abbbb",Salt = "S",RefreshToken="RT"},ListBanners=new List<Banner>(){ new Banner { IdAdvertisement = 1, Name = 1, Price = 100.99m, IdCampaing = 1, Area = 1000.0m } } },
                 new CampaignsResponse{ Campaign = new Campaign { IdCampaign = 2, IdClient = 2, StartDate = DateTime.Parse("2020-4-1"), EndDate = DateTime.Parse("2020-5-29"), PricePerSquareMeter = 5000.0m, FromIdBuilding = 3, ToldBuilding = 4 },
                    Client=new Client{ IdClient = 2, FirstName = "Jan", LastName = "A", Email = "Jan@maiil.com", Phone = "100 200 300", Login = "JanB", Password = "abbbb",Salt = "S2",RefreshToken="RT2"},ListBanners=new List<Banner>(){ new Banner { IdAdvertisement = 2, Name = 1, Price = 100.99m, IdCampaing = 2, Area = 1000.0m } } }
            });

            var controller = new AdvertApiController(DbLayer.Object);

            var result = controller.GetCampaigns();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            var ar = (IActionResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, ar.StatusCode);
        }

        public void GetCampaignsMethod_NoCampaigns_Correct(){
            var DbLayer = new Mock<IApiDbService>();

            List<CampaignsResponse> list = null;
            DbLayer.Setup(d => d.GetCampaigns()).Returns(list);

            var controller = new AdvertApiController(DbLayer.Object);

            var result = controller.GetCampaigns();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            var ar = (IActionResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, ar.StatusCode);
        }
    }
}
