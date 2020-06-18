using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Models.Events
{
	public class EventsIndexModel
	{
		public IEnumerable<EventsIndexListingModel> Events { get; set; }
		public IEnumerable<EventsPaidIndexListingModel> PaidEvents { get; set; }
		public string Search { get; set; }
	}
}
