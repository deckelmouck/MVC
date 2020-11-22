using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
	public class DeliveryPouch
	{
		public int DeliveryPouchID { get; set; }
		public int DeliveryID { get; set; }
		public int PouchID { get; set; }
		public int OrderQuantity { get; set; }
		public bool Delivered { get; set; }
		public virtual Pouch Pouch { get; set; }
		public virtual Delivery Delivery { get; set; }
	}
}
