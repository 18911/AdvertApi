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
    public class AdvertApiCampaignsIntegrationTest{

        [Test]
        public void GetCampaignsMethod_MoreThanOneCampaign_Correct() {
            var DbService = new EfApiDbService(new ApiDbContext());

            var controller = new AdvertApiController(DbService);

            var result = controller.GetCampaigns();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            var ar = (IActionResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, ar.StatusCode);
        }

        public void GetCampaignsMethod_NoCampaigns_Correct(){
            var DbService = new EfApiDbService(new ApiDbContext());
            var controller = new AdvertApiController(DbService);

            var result = controller.GetCampaigns();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            var ar = (IActionResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, ar.StatusCode);
        }
    }
}
