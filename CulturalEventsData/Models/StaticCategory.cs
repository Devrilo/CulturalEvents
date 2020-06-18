using System;
using System.Collections.Generic;
using System.Text;

namespace CulturalEventsData.Models
{
	public class StaticCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Event> Events { get; set; }
	}
}
