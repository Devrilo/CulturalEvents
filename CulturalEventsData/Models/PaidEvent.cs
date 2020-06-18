using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CulturalEventsData.Models
{
	public class PaidEvent
	{
		[Required]
		public int Id { get; set; }
		public int EventId { get; set; }
		public string TopText { get; set; }
		public string BottomText { get; set; }
		public Byte[] Picture { get; set; }
	}
}
