using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Models.Events
{
	public class EventsPaidIndexListingModel
	{
		public int Id { get; set; }
		public string TopText { get; set; }
		public string BottomText { get; set; }
		public Byte[] PaidPicture { get; set; }
	}
}
