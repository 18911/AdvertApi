using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Responses{
    public class NewCampaignResponse{
        public Campaign Campaign { get; set; }
        public List<Banner> ListBanners { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
