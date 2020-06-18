using CulturalEventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalEvents.Models.Events
{
	public class EventsEditModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public int HouseNumber { get; set; }
		public int ApartmentNumber { get; set; }
		public string PostalCode { get; set; }
		public string Description { get; set; }
		public int CreatorId { get; set; }
		public Byte[] Picture { get; set; }
		public EventAccount EventAccount { get; set; }
		public StaticCategory StaticCategory { get; set; }
		public int GetAccountsCount { get; set; }
	}
}
