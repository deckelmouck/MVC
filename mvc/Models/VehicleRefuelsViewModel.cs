using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class VehicleRefuelsViewModel
    {
        public Vehicle MyVehicle { get; set; }

        public List<Refuel> Refuels { get; set; }
    }
}
