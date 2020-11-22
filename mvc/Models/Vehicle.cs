using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int ConstructionYear { get; set; }
        public int Mileage { get; set; }
    }
}
