using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models{
    public class Campaign{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCampaign { get; set; }
        [ForeignKey("Client")]
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal PricePerSquareMeter { get; set; }

        [ForeignKey("Building")]
        public int FromIdBuilding { get; set; }

        [ForeignKey("Building")]
        public int ToldBuilding { get; set; }
    }
}
