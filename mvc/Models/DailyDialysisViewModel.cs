using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
	public class DailyDialysisViewModel
    {
        public DateTime DialysisDate { get; set; }
        public int Outweight { get; set; }
        public int Weight { get; set; }
        public int Diff { get; set; }
    }
}
