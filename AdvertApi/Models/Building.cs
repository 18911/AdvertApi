using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models{
    public class Building{
        [Key]
        public int IdBuilding { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }
        public int StreetNumber { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Height { get; set; }
    }
}
