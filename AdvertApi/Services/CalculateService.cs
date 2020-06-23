using AdvertApi.Models;
using AdvertApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt_test.Services{
    public class CalculateService{

        public static NewCampaignResponse GeCampaignSetup(List<Building> ListBuildings,decimal Price){
            List<List<Building>> ListBest = new List<List<Building>>();
            decimal BestArea = Decimal.MaxValue;
            var size = ListBuildings.Count() - 1;
            for (int i =0; i< size; i++) {
                List<Building> tmp1 = ListBuildings.GetRange(i,i+1);
                List<Building> tmp2 = ListBuildings.GetRange(i+1, ListBuildings.Count()-1);

                var r1 = CalculateArea(tmp1);
                var r2 = CalculateArea(tmp2);
                var result = r1 + r2;

                if (result > BestArea) {
                    BestArea = result;
                    ListBest[0] = tmp1;
                    ListBest[1] = tmp2;
                }
            }
            List<Banner> list = new List<Banner>();

            var b1Area = CalculateArea(ListBest[0]);
            var b1Price = b1Area * Price;
            
            var b2Area = CalculateArea(ListBest[1]);
            var b2Price = b2Area * Price;

            var TotalP = (b1Area + b2Area) * Price;

            var banner1 = new Banner { Name = 1, Area = b1Area,Price = b1Price};
            var banner2 = new Banner { Name = 2, Area = b2Area,Price = b2Price};

            list.Add(banner1);
            list.Add(banner2);

            return new NewCampaignResponse { ListBanners = list,TotalPrice = TotalP};
        }

        private static decimal CalculateArea(List<Building> list) {
            var maxHeight = list.Max(b=>b.Height);
            return maxHeight * list.Count();
        }
    }
}
