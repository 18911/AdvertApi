using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models{
    public class NewCampaignRequest{
        [Required]
        public int IdClient { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal PricePerSquareMeter { get; set; }
        [Required]
        public int FromIdBuilding { get; set; }
        [Required]
        public int ToIdBuilding { get; set; }
    }
}
