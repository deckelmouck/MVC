using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Dialysis
    {
        public int DialysisID { get; set; }
        public int PouchID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DialysisDate { get; set; }

        [DataType(DataType.Time)]
		public DateTime DialysisTime { get; set; }
		public int OutWeight { get; set; }
        public virtual Pouch Pouch { get; set; }
    }
}
