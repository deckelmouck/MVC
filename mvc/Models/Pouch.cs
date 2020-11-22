using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Pouch
    {
        public int PouchID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Weight { get; set; }
        public int UsagePerDay { get; set; }
        public int StockOut { get; set; }
    }
}
