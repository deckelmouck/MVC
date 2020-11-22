using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
	public class PouchViewModel : Pouch
	{
		public Pouch pouch { get; set; }

		public int RemainingDays 
		{ 
			get 
			{ 
				return StockOut / UsagePerDay; 
			} 
		}
	}
}
