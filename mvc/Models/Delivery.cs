using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
	public class Delivery
	{
		public int DeliveryID { get; set; }

		[DataType(DataType.Date)]
		public DateTime DeliveryDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime LastOrderDate { get; set; }
		public bool Delivered { get; set; }
	}
}
