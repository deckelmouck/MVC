using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Refuel
    {
        public int RefuelID { get; set; }
        public int VehicleID { get; set; }

        [DataType(DataType.Date)]
        public DateTime RefuelDate { get; set; }

        //[RegularExpression(@"^\d+\,\d{0,2}$")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
