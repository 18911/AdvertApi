using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models{
    public class Banner{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdvertisement { get; set; }
        public int Name { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [ForeignKey("Campaign")]
        public int IdCampaing { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Area { get; set; }
    }
}
