using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData.Models
{
	public class EventAccount
	{	
		public int Id { get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }
		public int AccountId { get; set; }
		public Account Account { get; set; }
	}
}
